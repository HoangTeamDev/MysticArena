
using Menu.System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

namespace UIScripts.SystemUI
{
    public enum UIKey
    {
        HUD, Window, Popup
    }
    public enum WindowType
    {
        Shop,
        Inventory,
        Chest,
        DialogNPC,
        PlayerHUD,
        ActionHUD,
        ShowItem,
        Mission,
        Input,
        PlayerDie,
        InfoPlayer,
        Equipment
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
        public virtual void OpenMe() { /*UIManager.Instance.Open(WindowType);*/ }

        public virtual void Close() { /*gameObject.SetActive(false); */}

        public virtual void CloseMe() { /*UIManager.Instance.Close(WindowType);*/ }
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
