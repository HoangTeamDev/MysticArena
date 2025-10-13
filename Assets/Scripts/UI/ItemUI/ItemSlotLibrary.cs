using System.Collections;
using System.Collections.Generic;
using UI.SystemUI;
using UI.UIOvelay;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.ItemUI
{
    public class ItemSlotLibrary : ItemSlotBase, IPointerClickHandler
    {
        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {

            base.OnPointerClick(eventData);
           
        }

        public override void Onselect()
        {
            base.Onselect();
        }

        public override void OnSelect(BaseEventData baseEventData)
        {
            base.OnSelect(baseEventData);
        }

        public override void SeletecMe()
        {
            base.SeletecMe();
            switch (typeItemSlot)
            {
                case TypeItemSlot.Library:
                    UIInfoCard uIInfoCard = UIController.Instance.Get<UIInfoCard>(WindowType.UI_InfoCard);
                    if (uIInfoCard != null)
                    {
                        uIInfoCard.Show(ItemBase.cardData);
                        uIInfoCard.OpenMe();
                        
                    }
                    break;
            }
        }
    }
}   

