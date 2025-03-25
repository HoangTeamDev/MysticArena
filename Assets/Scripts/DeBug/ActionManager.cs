using System;
using System.Collections.Generic;

public static class ActionManager
{
    private static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    public static void Subscribe(string key, Delegate callback)
    {
        if (!eventTable.ContainsKey(key))
            eventTable[key] = null;

        eventTable[key] = Delegate.Combine(eventTable[key], callback);
    }

    public static void Unsubscribe(string key, Delegate callback)
    {
        if (eventTable.ContainsKey(key))
        {
            eventTable[key] = Delegate.Remove(eventTable[key], callback);
            if (eventTable[key] == null)
                eventTable.Remove(key);
        }
    }

    public static void Invoke(string key, params object[] parameters)
    {
        if (eventTable.TryGetValue(key, out var del))
        {
            try
            {
                del.DynamicInvoke(parameters);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogWarning($"Invoke error for '{key}': {e.Message}");
            }
        }
    }
    // ====== Create + Auto Subscribe ======
    public static void CreateAction(string key, Delegate callback)
    {
        Subscribe(key, callback);
    }

    public static void Clear()
    {
        eventTable.Clear();
    }
}
