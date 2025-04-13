using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Progress;

using UnityEngine.EventSystems;

using UnityEngine.UI;

namespace UI.SystemUI
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        public static bool HasInstance => Instance != null;
        
        [SerializeField]private Dictionary<WindowType, UIBase> _windows = new Dictionary<WindowType, UIBase>();
        public UIBase _UiWindowCurrent;
        public UIBase _popUpCurrent;
        public bool IsOpenUI;
        public List<UIBase> _windowsList;
        public CanvasScaler _CanvasScaler;                
        private void Start()
        {
            RegisterAllWindows();
            IsOpenUI = false;
        }
       
             
        public List<string> SplitByNewline(string input)
        {
            return input.Split(new[] { '\n' }, System.StringSplitOptions.None).ToList();
        }
        public Vector2 GetResCanvaScale()
        {
            return _CanvasScaler.referenceResolution;
        }
      
        private void RegisterAllWindows()
        {

            foreach (var window in _windowsList)
            {
                WindowType key = window.WindowType;
                if (!_windows.ContainsKey(key))
                {
                    _windows.Add(key, window);
                    window.Init();
                }
               

            }
        }

        public void RegisterWindow(WindowType key, UIBase window)
        {
            if (!_windows.ContainsKey(key))
                _windows.Add(key, window);
        }

        public T Get<T>(WindowType key) where T : UIBase
        {
            if (_windows.TryGetValue(key, out var win))
            {
                if (win is T typedWindow)
                {
                    return typedWindow;
                }
                else
                {
                    MainLog.LogError("[UIManager] Get Fail Because wrong type", $"{key} is not of type {typeof(T).Name}", ReadColor.Yellow);
                }
            }
            else
            {
                MainLog.LogError("[UIManager] Get Fail Because not found", $"{key}", ReadColor.Cyan);
            }
            return null;
        }
        public void Open(WindowType key)
        {         
            if (_windows.TryGetValue(key, out var win))
            {
                if (win.Key == UIKey.Window)
                {
                    if (_UiWindowCurrent != null)
                    {
                        _UiWindowCurrent.Close();
                    }
                    _UiWindowCurrent = win;
                    win.Open();
                }
                if (win.Key == UIKey.Popup)
                {
                    if (_popUpCurrent != null)
                    {
                        _popUpCurrent.Close();
                    }
                    _popUpCurrent = win;
                    win.Open();
                }
                IsOpenUI = true;
            }
            else
            {
                MainLog.LogError("[UIManager] open Fail Because not found", $"{key}", ReadColor.Cyan);
            }
        }
        public void OpenWindowCurrent()
        {
            if (_UiWindowCurrent == null) return;
            _UiWindowCurrent.Open();
            IsOpenUI = true;
        }
        public void Close(WindowType key)
        {

            if (_windows.TryGetValue(key, out var win))
            {
                if (win.Key == UIKey.Window)
                {
                    _UiWindowCurrent = null;
                    win.Close();
                }
                if (win.Key == UIKey.Popup)
                {

                    win.Close();
                    _popUpCurrent = null;
                }
                IsOpenUI = false;
            }
            else
            {
                MainLog.LogError("[UIManager] Close Fail Because not found", $"{key}", ReadColor.Cyan);
            }
            
        }

        public void Toggle(WindowType key)
        {
            if (_windows.TryGetValue(key, out var win))
            {
                _UiWindowCurrent.Close();
                win.Open();
                _UiWindowCurrent = win;
            }
            else
            {
                MainLog.LogError("[UIManager] Toggle Fail Because not found", $"{key}", ReadColor.Cyan);
            }
        }

        public bool IsOpen(WindowType key)
        {
            return _windows.ContainsKey(key) && _windows[key].isOpen && _UiWindowCurrent.WindowType == key;
        }
        public void SetSelectUICurrent(GameObject gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        public void CloseAllHUD()
        {
            foreach (var win in _windows)
            {
                if (win.Value.Key == UIKey.HUD && win.Value.isOpen)
                {
                    win.Value.Close();
                }
            }
            if (_UiWindowCurrent != null && _UiWindowCurrent.Key == UIKey.HUD)
            {
                _UiWindowCurrent = null;
                IsOpenUI = false;
            }
        }
        public void CloseAllWindow()
        {
            foreach (var win in _windows)
            {
                if (win.Value.Key == UIKey.Window && win.Value.isOpen)
                {
                    win.Value.Close();
                }
            }
            if (_UiWindowCurrent != null && _UiWindowCurrent.Key == UIKey.Window)
            {
                _UiWindowCurrent = null;
                IsOpenUI = false;
            }
        }
        public void CloseAllOverlay()
        {
            foreach (var win in _windows)
            {
                if (win.Value.Key == UIKey.Popup && win.Value.isOpen)
                {
                    win.Value.Close();
                }
            }
            if (_UiWindowCurrent != null && _UiWindowCurrent.Key == UIKey.Popup)
            {
                _UiWindowCurrent = null;
                IsOpenUI = false;
            }
        }
        public void CloseAll()
        {
            foreach (var win in _windows.Values)
                win.Close();
            IsOpenUI = false;
        }

    }
}

