using CardData;
using Menu.Connet;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UI.SystemUI;
using UI.UIOvelay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.ItemUI
{
    public class ItemSlotLibrary : ItemSlotBase, IPointerClickHandler
    {
        public TextMeshProUGUI _namecard;
        public Image _icon;
        
        public async void Init()
        {
            _namecard.text=card._Name;
           
            _icon.sprite = await GameData.Instance.LoadAsset<Sprite>($"{card._CardId}");
           // _icon.sprite = Resources.Load<Sprite>($"Sprite/Item/{card._CardId}");
        }
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
                    ClientMain.Instance.SendEffCard(card._CardId);
                   
                    break;
            }
        }
    }
}   

