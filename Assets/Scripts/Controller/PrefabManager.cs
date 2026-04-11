using Menu.System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIScripts.SystemUI;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[System.Serializable]
public class Prefab
{
    public GameObject prefab;
    public KeyPrefab KeyPrefab;
}

public enum KeyPrefab
{
    Player,
    Enemy,
    NPC,
    Box,
    Item,
    TextPopUp,
    Pet,
    BulletEnemy,BoxLefft, BoxRight, BulletSkill77, BulletSkill79, BulletSkill80,BulletSkill82, BulletSkill83, BulletSkill43, BulletSkill21, BulletSkill30, BulletSkill33,
    BulletSkill521, BulletSkill522, BulletSkill71, BulletSkill85
    ,BulletSkillBoss113, BulletSkillBoss136, BulletSkill821, BulletSkill822, BulletSkill823, BulletSkill81, BulletSkill78, BulletSkill65, BulletSkill62,
    BulletSkill27, BulletSkill84, BlastSkill78, BulletSkillBoss115, BulletSkillBoss90, BulletSkillBoss120, BulletSkillBoss9022, BulletSkillBoss117,
    BulletSkillBoss9043, BulletSkillBoss123, BulletSkillBoss205, BulletSkillBoss121, BulletSkillBoss203, BulletSkillBoss9044,
    BulletSkill233, BulletSkill231, BulletSkill232, BulletSkill236, BulletSkill234, BulletSkill235, BulletSkill237,BulletSkill238, BulletSkill239
}

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    // Dictionary lưu prefab
    [ShowInInspector, ReadOnly]
    private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    [SerializeField] private List<Prefab> _listPrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var prefab in _listPrefab)
        {
            Register(prefab.KeyPrefab.ToString(), prefab.prefab);
        }
    }

    /// <summary>
    /// Đăng ký prefab vào manager
    /// </summary>
    public void Register(string key, GameObject prefab)
    {
        if (prefabDictionary.ContainsKey(key))
        {
            MainLog.LogWarning($"PrefabManager: Key '{key}'", $"đã được đăng ký rồi!", ReadColor.Red);
            return;
        }

        prefabDictionary.Add(key, prefab);
    }

    /// <summary>
    /// Lấy prefab từ manager
    /// </summary>

    public async Task<GameObject> GetPrefab(string key)
    {
        try
        {
            if (!prefabDictionary.TryGetValue(key, out GameObject prefab))
            {
                GameObject gameObject = await AddressablesManager.Instance.LoadAssetAsync<GameObject>(key);

                if (!prefabDictionary.ContainsKey(key))
                    prefabDictionary.Add(key, gameObject);

                return prefabDictionary[key];
            }

            return prefab;
        }
        catch (System.Exception ex)
        {
            MainLog.LogError($"PrefabManager: Lỗi khi lấy prefab với key '{key}': {ex.Message}", "", ReadColor.Red);
            return null;

        }
    } 

    /// <summary>
    /// Kiểm tra prefab đã có trong manager chưa
    /// </summary>
    public bool HasPrefab(string key)
    {
        return prefabDictionary.ContainsKey(key);
    }
}
