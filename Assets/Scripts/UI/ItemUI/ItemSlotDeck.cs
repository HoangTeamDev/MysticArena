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
using UI.UIWindow;
namespace UI.ItemUI
{
    public class ItemSlotDeck : ItemSlotBase,IPointerDownHandler,IPointerUpHandler
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
        [SerializeField] private bool isHolding;
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
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
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
                    UIReview uIReview = UIController.Instance.Get<UIReview>(WindowType.UI_Review);
                    if (uIReview != null)
                    {
                        ButtonConfirm item = uIReview.CreateButtonconfirm();
                        item._des.text = "Thông Tin";
                        item.button.onClick.AddListener(() =>
                        {
                            ClientMain.Instance.SendEffCard(card._CardId);
                            uIReview.CloseMe();
                        });
                        switch (type)
                        {
                            case 1:
                                {
                                    ButtonConfirm item1 = uIReview.CreateButtonconfirm();
                                    item1._des.text = "Thêm";
                                    item1.button.onClick.AddListener(() =>
                                    {
                                        ClientMain.Instance.AddCardToDeck(card._CardId);
                                        uIReview.CloseMe();
                                    });
                                }
                               
                                break;
                            case 2:
                                {
                                    ButtonConfirm item1 = uIReview.CreateButtonconfirm();
                                    item1._des.text = "Bỏ ra";
                                    item1.button.onClick.AddListener(() =>
                                    {
                                        ClientMain.Instance.RemoveCardFromDeck(card._CardId);
                                        uIReview.CloseMe();
                                    });
                                }
                               
                                break;
                        }
                       
                        uIReview.OpenMe();
                    }
             
                   
                    break;
            }
        }
    }

}
