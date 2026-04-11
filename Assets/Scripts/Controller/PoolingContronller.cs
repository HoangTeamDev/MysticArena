using Cysharp.Threading.Tasks;
using Menu.System;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UIScripts.SystemUI;
using UnityEngine;

public class PoolingContronller : Singleton<PoolingContronller>
{
    [ShowInInspector, ReadOnly]
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    [ShowInInspector, ReadOnly]
    private Dictionary<GameObject, Transform> poolParentDictionary = new Dictionary<GameObject, Transform>();

    /// <summary>
    /// Spawn object từ pool
    /// </summary>
    public GameObject Spawn(GameObject prefab, Vector2 position)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary[prefab] = new Queue<GameObject>();
            CreatePoolParent(prefab);
        }

        GameObject obj;

        if (poolDictionary[prefab].Count > 0)
        {
            obj = poolDictionary[prefab].Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
            obj.AddComponent<PooledObject>().originalPrefab = prefab;         
            obj.transform.SetParent(poolParentDictionary[prefab]);
        }
        PooledObject pooled = obj.GetComponent<PooledObject>();
        if (pooled == null)
        {
            pooled = obj.AddComponent<PooledObject>();
            pooled.originalPrefab = prefab;
        }
        obj.transform.position = position;      
        obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Despawn object → trả về pool
    /// </summary>
    public void Despawn(GameObject obj)
    {
        if (obj == null) return;

        PooledObject pooledObj = obj.GetComponent<PooledObject>();

        if (pooledObj == null || pooledObj.originalPrefab == null)
        {
            MainLog.LogWarning(
                $"Đang cố gắng despawn object {obj.name} !",
                "",
                ReadColor.Red
            );
            Destroy(obj);
            return;
        }

        if (pooledObj.IsDespawning)
            return;

        pooledObj.IsDespawning = true;

        if (!poolDictionary.TryGetValue(pooledObj.originalPrefab, out var queue))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(poolParentDictionary[pooledObj.originalPrefab]);
        queue.Enqueue(obj);

        pooledObj.IsDespawning = false;
    }
    /// <summary>
    /// Despawn object sau delay X giây
    /// </summary>
    public void DespawnDelay(GameObject obj, float delay)
    {
        if (obj == null) return;

        PooledObject pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj == null || pooledObj.IsDespawning)
            return;

        pooledObj.IsDespawning = true;
        DespawnDelayAsync(obj, delay).Forget();
    }

    private async UniTask DespawnDelayAsync(GameObject obj, float delay)
    {
        await UniTask.Delay(
            TimeSpan.FromSeconds(delay),
            cancellationToken: obj.GetCancellationTokenOnDestroy()
        );

        if (obj == null || !obj.activeInHierarchy)
            return;

        pooledObjSafe(obj);
    }

    private void pooledObjSafe(GameObject obj)
    {
        PooledObject pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj == null) return;

        pooledObj.IsDespawning = false;
        Despawn(obj);
    }
    /// <summary>
    /// Tạo parent GameObject cho prefab (1 lần duy nhất)
    /// </summary>
    private void CreatePoolParent(GameObject prefab)
    {
        GameObject parentObj = new GameObject($"Pool_{prefab.name}");
        parentObj.transform.SetParent(this.transform);
        poolParentDictionary[prefab] = parentObj.transform;
    }
}
