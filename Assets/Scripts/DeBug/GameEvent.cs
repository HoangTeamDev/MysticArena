using System;
using System.Collections.Generic;
using UnityEngine;

public enum ListEvent
{
    OnClick
}

public class GameEvent : MonoBehaviour
{
    public static GameEvent Instance;

    public static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        //CreateAction(ListEvent.OnClick.ToString(), (Action<int>)(null));

    }
    
 
    //====== Đăng ký sự kiện ======
    public void Register(string key, Delegate callback)
    {
        if (!eventTable.ContainsKey(key))
            eventTable[key] = null;

        eventTable[key] = Delegate.Combine(eventTable[key], callback);
    }

    // ====== Hủy đăng ký ======
    public static void Subscribe(string key, Delegate callback)
    {
        if (!eventTable.ContainsKey(key))
            eventTable[key] = null;

        eventTable[key] = Delegate.Combine(eventTable[key], callback);
    }
    public static void CreateAction(string key, Delegate callback)
    {
        Subscribe(key, callback);
    }
    public void Unregister(string key, Delegate callback)
    {
        if (eventTable.ContainsKey(key))
        {
            eventTable[key] = Delegate.Remove(eventTable[key], callback);
            if (eventTable[key] == null)
                eventTable.Remove(key);
        }
    }

    // ====== Gọi sự kiện ======
    public void Trigger(string key, params object[] parameters)
    {
        if (eventTable.TryGetValue(key, out var del))
        {
            try
            {
                del.DynamicInvoke(parameters);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[GameEvent] Trigger error for '{key}': {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"[GameEvent] Event '{key}' not found.");
        }
    }

    // ====== Xóa sự kiện ======
    public void RemoveEvent(string key)
    {
        eventTable.Remove(key);
    }

    // ====== Xóa toàn bộ ======
    public void Clear()
    {
        eventTable.Clear();
    }
}
