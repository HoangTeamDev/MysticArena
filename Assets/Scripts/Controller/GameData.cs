using CardData;
using Cysharp.Threading.Tasks;
using Menu.System;
using Player;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class GameData : Singleton<GameData>
{
    public PlayerData _mainPlayer;
    public List<Card> _allCard;
    [ShowInInspector, ReadOnly] private Dictionary<string, Sprite> _spriteCache = new();
    [ShowInInspector, ReadOnly] private Dictionary<string, GameObject> _prefabCache = new();
    
    public Card GetCardByID(int id)
    {
        return _allCard.Find(c => c._CardId == id);
    }
    public async UniTask<T> LoadAsset<T>(string key, CancellationToken ct = default) where T : UnityEngine.Object
    {

        if (typeof(T) == typeof(Sprite))
        {
            if (_spriteCache.TryGetValue(key, out var sp))
                return sp as T;

            var asset = await AddressablesManager.Instance.LoadAssetAsync<Sprite>(key, ct);
            if (asset != null)
                _spriteCache[key] = asset;

            return asset as T;
        }


        if (typeof(T) == typeof(GameObject))
        {
            if (_prefabCache.TryGetValue(key, out var go))
                return go as T;

            var asset = await AddressablesManager.Instance.LoadAssetAsync<GameObject>(key, ct);
            if (asset != null)
                _prefabCache[key] = asset;

            return asset as T;
        }


       


        MainLog.LogError($"Chưa hỗ trợ type", $"{typeof(T)}", ReadColor.Gold);
        return null;
    }
}
