using Menu.System;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace UI.SystemUI
{
    public enum ListEvent
    {
        OnClick,
        LoadMap,
        UpdatePlayer,
        UPdateOther,
        PlayerLive,
        PlayerDie,
        Currency,
        AddItemInventory,
        UpdateItemBody


    }

    public class GameEvent : MonoBehaviour
    {
        public static GameEvent Instance;
        private Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);


        }

        public bool HasInstance => Instance != null;
        public void Subscribe(string key, Action callback)
        {
            AddDelegate(key, callback);
        }

        public void Subscribe<T>(string key, Action<T> callback)
        {
            AddDelegate(key, callback);
        }

        public void Subscribe<T1, T2>(string key, Action<T1, T2> callback)
        {
            AddDelegate(key, callback);
        }
        public void Subscribe<T1, T2, T3>(string key, Action<T1, T2, T3> callback)
        {
            AddDelegate(key, callback);
        }

        private void AddDelegate(string key, Delegate callback)
        {
            if (eventTable.ContainsKey(key))
            {
                if (eventTable[key].GetType() != callback.GetType())
                {
                    MainLog.LogError("[GameEvent]", $"{key}. Expected: {eventTable[key].GetType()}, got: {callback.GetType()}", ReadColor.Red);
                    return;
                }
                eventTable[key] = Delegate.Combine(eventTable[key], callback);
            }
            else
            {
                eventTable.Add(key, callback);
            }

            MainLog.Log($"Đăng Ký '{key}' Thành Công", $"Type: {callback.GetType()}", ReadColor.Green);
        }



        private void UnsubscribeInternal(string key, Delegate callback)
        {
            if (!eventTable.ContainsKey(key)) return;

            if (eventTable[key].GetType() != callback.GetType())
            {
                MainLog.LogError("[GameEvent] khi hủy", $"{key}. Expected: {eventTable[key].GetType()}, got: {callback.GetType()}", ReadColor.Red);
                return;
            }

            eventTable[key] = Delegate.Remove(eventTable[key], callback);

            if (eventTable[key] == null)
                eventTable.Remove(key);

            MainLog.Log($"Hủy Đăng Ký Thành Công", $"{key}", ReadColor.Orange);
        }
        public void Unsubscribe(string key, Action callback)
        {
            UnsubscribeInternal(key, callback);
        }

        public void Unsubscribe<T>(string key, Action<T> callback)
        {
            UnsubscribeInternal(key, callback);
        }

        public void Unsubscribe<T1, T2>(string key, Action<T1, T2> callback)
        {
            UnsubscribeInternal(key, callback);
        }

        public void Unsubscribe<T1, T2, T3>(string key, Action<T1, T2, T3> callback)
        {
            UnsubscribeInternal(key, callback);
        }

        public void Trigger(string key)
        {
            if (eventTable.TryGetValue(key, out var del) && del is Action action)
            {
                action.Invoke();
            }
        }

        public void Trigger<T>(string key, T param)
        {
            if (eventTable.TryGetValue(key, out var del) && del is Action<T> action)
            {
                action.Invoke(param);
            }
        }

        public void Trigger<T1, T2>(string key, T1 param1, T2 param2)
        {
            if (eventTable.TryGetValue(key, out var del) && del is Action<T1, T2> action)
            {
                action.Invoke(param1, param2);
            }
        }
        public void Trigger<T1, T2, T3>(string key, T1 param1, T2 param2, T3 param3)
        {
            if (eventTable.TryGetValue(key, out var del) && del is Action<T1, T2, T3> action)
            {
                action.Invoke(param1, param2, param3);
            }
        }


        public void RemoveEvent(string key)
        {
            if (eventTable.ContainsKey(key))
                eventTable.Remove(key);
        }


        public void Clear()
        {
            eventTable.Clear();
        }
    }
}

