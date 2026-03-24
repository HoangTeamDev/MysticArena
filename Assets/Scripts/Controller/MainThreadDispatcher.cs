using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour {
    private static readonly ConcurrentQueue<Action> _executionQueue = new ConcurrentQueue<Action>();
    public static Queue<Action> actions = new Queue<Action>();
    protected void Update() {
        while (_executionQueue.TryDequeue(out var action)) {
            try {
                action?.Invoke();
            } catch (Exception ex) {
                Debug.LogException(ex);
            }
        }
        lock (actions)
        {
            while (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
        }
    }

    public static void Enqueue(Action action) {
        if (action == null)
            return;
        _executionQueue.Enqueue(action);
    }
    public static void ClearMessage() {
     
        
        _executionQueue.Clear();
    }


    public static void RunOnMainThread(Action action)
    {
        lock (actions)
        {
            actions.Enqueue(action);
        }
    }
}