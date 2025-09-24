using System.Collections;
using System.Collections.Generic;
using System.Text;
using UI.ItemUI;
using UI.SystemUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.UIWindow
{
    public enum TypeItemSlot { Inventory, Equiment, Upgrade, Skill, Shop }
    public class UIInventory : UIBase
    {
        public ItemSlotBase _itemPre;
        public List<ItemSlotBase> _listItem;
        public GameObject _conten;

        public override void Init()
        {
            base.Init();
        }

        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

        public override void Open()
        {
            base.Open();
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }

       

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        public override void Close()
        {
            base.Close();
        }

        public override void CloseMe()
        {
            base.CloseMe();
        }
    }
}

