
using Menu.System;
using UI.ItemUI;
using UI.SystemUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.SystemUI
{
    public enum UIKey
    {
        HUD, Window, Popup
    }
    public enum WindowType
    {
       PlayerHUD,
       Inventory,
       Shop,
       Library,
       InfoCard

    }
    public abstract class UIBase : MonoBehaviour, IPointerClickHandler
    {
        public UIKey Key;
        public WindowType WindowType;
        public bool isOpen;
        public bool isHide;
        public bool isNeverHide;
        public virtual void Init()
        {
            MainLog.Log($"✅ Khởi tạo thành công 'UIKey': [{Key}]", $"WindowType: {WindowType}", ReadColor.Turquoise);
            if (!isNeverHide)
            {
                gameObject.SetActive(false);
                isOpen = false;
            }
            else
            {
                isOpen = true;
            }

        }
        public virtual void Open() { gameObject.SetActive(true); }
        public virtual void OpenMe() { UIController.Instance.Open(WindowType); }

        public virtual void Close() { }

        public virtual void CloseMe() { UIController.Instance.Close(WindowType); }
        public virtual void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.pointerCurrentRaycast.gameObject == this.gameObject && isHide && !isNeverHide)
            {
                CloseMe();
            }
        }

        public virtual void ShowInfoItem(System.Text.StringBuilder sb, Item item)
        {

        }
        protected virtual void OnEnable() { }


        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

    }
}
