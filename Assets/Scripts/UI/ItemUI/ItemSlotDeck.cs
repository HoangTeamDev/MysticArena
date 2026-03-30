using ExitGames.Client.Photon;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UI.SystemUI;
using UI.UIOvelay;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Menu.Connet;
namespace UI.ItemUI
{
    public class ItemSlotDeck : ItemSlotBase
    {
        [SerializeField] private Image _imageCard;
        [SerializeField] private TextMeshProUGUI _namecard;
        [SerializeField] public TextMeshProUGUI _numberCard;
        [SerializeField] List<Image> _level;
        [SerializeField] private Image _elementCard;
        [SerializeField] private Image _rateCard;
        [SerializeField] private Image _rateBackgroundCard;
        [SerializeField] private TextMeshProUGUI _atk;
        [SerializeField] private TextMeshProUGUI _hp;
        public int type;
        public async void Init()
        {
            if (card != null)
            {
                if(card._CardType is 1)
                {
                    _atk.text = card._Attack.ToString();
                    _hp.text = card._Hp.ToString();
                    _elementCard.sprite = await GameData.Instance.LoadAsset<Sprite>("E" + card._Element);
                    for (int i = 0; i < card._Level; i++)
                    {
                        _level[i].gameObject.SetActive(true);
                    }
                }
                _namecard.text = card._Name;             
                _imageCard.sprite = await GameData.Instance.LoadAsset<Sprite>(card._CardId.ToString());
                _rateCard.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{card.GetRate()}");
                _rateBackgroundCard.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{card.GetRate()}");


            }
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
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                switch (type)
                {
                    case 1:
                        ClientMain.Instance.AddCardToDeck(card._CardId);
                        break;
                    case 2:
                        ClientMain.Instance.RemoveCardFromDeck(card._CardId);
                        break;
                }
            }
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
                case TypeItemSlot.Deck:
                    UIInfoCard uIInfoCard = UIController.Instance.Get<UIInfoCard>(WindowType.UI_InfoCard);
                    if (uIInfoCard != null)
                    {
                        uIInfoCard.ShowItem(card);
                        uIInfoCard.OpenMe();

                    }
                    break;
            }
        }
    }

}
