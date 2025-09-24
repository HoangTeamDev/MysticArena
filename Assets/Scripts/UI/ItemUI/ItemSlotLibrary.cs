using System.Collections;
using System.Collections.Generic;
using UI.SystemUI;
using UI.UIOvelay;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.ItemUI
{
    public class ItemSlotLibrary : ItemSlotBase
    {
        public override void OnPointerClick(PointerEventData eventData)
        {

            base.OnPointerClick(eventData);
            if (ItemBase != null)
            {
                UIInfoCard uIInfoCard = UIController.Instance.Get<UIInfoCard>(WindowType.UI_InfoCard);
            }
        }
    }
}   

