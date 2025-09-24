using Menu.Connet;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.SystemUI;
using UI.UIOvelay;
using UI.UIWindow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.ItemUI
{
    public abstract class ItemSlotBase : IPointerClickHandler
    {
        public Button Button;
        public ItemBase ItemBase;
        public TypeItemSlot typeItemSlot;
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            
        }
        public virtual void Onselect()
        {
            SeletecMe();
        }
        public void SeletecMe()
        {
            
        }
    }
}

