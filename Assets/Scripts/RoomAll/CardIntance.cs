using CardData;
using DG.Tweening;
using Menu.Connet;
using System.Collections.Generic;

using TMPro;
using UI.ItemUI;
using UI.SystemUI;
using UI.UIOvelay;
using UI.UIWindow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace RoomAll
{
    public enum ZoneType
    {
        Deck,
        Hand,
        Monster,
        SpellTrap,
        Graveyard,
        Banished
    }
    public enum PhaseType
    {
        Start,
        Draw,
        Main,
        End
    }
    [System.Serializable]
    public class CardIntance: ItemSlotBase
    {
        public Card Card;
        public long InstanceId;
        public int CardId;

        public int OwnerPlayerId;
        public int ControllerPlayerId;

        public ZoneType CurrentZone;
        public int SlotIndex;

        public bool IsFaceUp;
        public int CurrentAtk;
        public int CurrentHp;
        public bool HasAttack;
        [Header("thuoctinh")]
        [Header("Monster")]
        public Image imageMonter;
        public Image element;
        public Image Rate;
        public Image BackGroundRate;
        public List<Image> imagesLevel;
        public TextMeshProUGUI nameMonter;

        public TextMeshProUGUI ATK;
        public TextMeshProUGUI HP;
        [Header("Spell")]
        public Image imageSpell;
        public TextMeshProUGUI nameSpell;
        public GameObject spellCard;
        public Image RateSpell;
        public Image BackGroundRateSpell;
        [Header("Trap")]
        public Image imageTrap;
        public TextMeshProUGUI nameTrap;
        public GameObject trapCard;
        public Image RateTrap;
        public Image BackGroundRateTrap;
        public Canvas canvas;
        public Vector2 localPos=new Vector2();
        public int currentorder;
        public Button _btnActive;
        public GameObject _frotCard;
        public GameObject _BackGround;
        public async void LoadIcon()
        {
            switch (this.Card._CardType)
            {
                case 1:
                    imageMonter.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());

                    break;
                case 2:
                    imageSpell.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());

                    break;
                case 3:
                    imageTrap.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());

                    break;
            }
        }
       
        public async void Init()
        {
            GameEvent.Instance.Subscribe<ItemSlotBase>(ListEvent.SelectCard.ToString(), SeletecMe);
            switch (this.Card._CardType)
            {
                case 1:
                    {
                        nameMonter.text = this.Card._Name;
                        ATK.text = this.Card._Attack.ToString();
                        HP.text = this.Card._Hp.ToString();
                        for (int i = 0; i < this.Card._Level; i++)
                        {
                            imagesLevel[i].gameObject.SetActive(true);
                        }
                        element.sprite = await GameData.Instance.LoadAsset<Sprite>("E" + this.Card._Element);
                        imageMonter.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());
                        Rate.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRate.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}"); ;
                    }
                    break;
                case 2:
                    {
                        nameSpell.text = this.Card._Name;
                        RateSpell.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRateSpell.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}");
                        imageSpell.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());
                    }
                    break;
                case 3:
                    {
                        nameTrap.text = this.Card._Name;
                        RateTrap.sprite = await GameData.Instance.LoadAsset<Sprite>($"Rate{this.Card.GetRate()}");
                        BackGroundRateTrap.sprite = await GameData.Instance.LoadAsset<Sprite>($"BR{this.Card.GetRate()}");
                        imageTrap.sprite = await GameData.Instance.LoadAsset<Sprite>(this.Card._CardId.ToString());
                    }
                    break;
            }
        }
        public void SummonToSlot(int slot)
        {
            UIMainField ui = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
            if (ui == null) return;

            Transform startParent = rectTransform.parent;

            // Đưa lên layer cao nhất để không bị che
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = 999;
            }

            // Bay lên giữa màn
            rectTransform.SetParent(ui._point3);
            rectTransform.SetAsLastSibling();

            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);

            Sequence seq = DOTween.Sequence();

           
            seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.35f).SetEase(Ease.OutCubic).OnComplete(() => {

                rectTransform.DOScale(1.3f, 0.35f).OnComplete(() =>
                {
                   
                    rectTransform.DOScale(1f, 0.1f).OnComplete(() =>
                    {
                        rectTransform.DOScale(1.3f, 0.15f);
                    });

                });
            }));


           
            seq.AppendInterval(1f);

           
            seq.AppendCallback(() =>
            {
                rectTransform.SetParent(ui.monsterZoneMe[slot]);
                rectTransform.SetAsLastSibling();
            });

            seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.InQuad));
            seq.Join(rectTransform.DORotate(Vector3.zero, 0.25f));

            // 🪵 Phase 4: nảy nhẹ (impact feel)
            seq.Append(rectTransform.DOScale(0.8f, 0.15f).SetEase(Ease.InQuad));
            seq.Append(rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));

            // 🎯 Kết thúc
            seq.OnComplete(() =>
            {
                rectTransform.localScale = Vector3.one ;
               
                // reset sorting
                if (canvas != null)
                {
                    canvas.sortingOrder = currentorder;
                }

                // remove khỏi hand
                ui._cardRowLayoutMe.cards.Remove(rectTransform);
                ui._cardRowLayoutMe.cardIntances.Remove(this);
                ui._cardRowLayoutMe.UpdateCard();
            });
        }

        public void EnemySummon(int slot)
        {
            UIMainField ui = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
            if (ui == null) return;

            Transform startParent = rectTransform.parent;

            // Đưa lên layer cao nhất để không bị che
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = 999;
            }

            // Bay lên giữa màn
            rectTransform.SetParent(ui._point3);
            rectTransform.SetAsLastSibling();

            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);

            Sequence seq = DOTween.Sequence();

           
            seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.35f).SetEase(Ease.OutCubic).OnComplete(() =>{

                rectTransform.DOScale(1.3f, 0.35f).OnComplete(() =>
                {
                    Vector3 vector3 = rectTransform.localScale;
                    rectTransform.DOScaleX(0, 0.2f).OnComplete(() =>
                    {
                        _frotCard.SetActive(true);
                        _BackGround.SetActive(false);

                        rectTransform.DOScaleX(vector3.x, 0.2f);
                        rectTransform.DOScale(1f, 0.1f).OnComplete(() =>
                        {
                            rectTransform.DOScale(1.3f, 0.15f);
                        });

                    });
                   
                });
            }));
          
           
           
           
            

           
            seq.AppendInterval(1f);

           
            seq.AppendCallback(() =>
            {
                rectTransform.SetParent(ui.monsterZoneOther[slot]);
                rectTransform.SetAsLastSibling();
               
            });
           
            seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.InQuad));
           

           
            seq.Append(rectTransform.DOScale(0.8f, 0.15f).SetEase(Ease.InQuad));
            seq.Append(rectTransform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));

            // 🎯 Kết thúc
            seq.OnComplete(() =>
            {
                rectTransform.localScale = Vector3.one;

                
                if (canvas != null)
                {
                    canvas.sortingOrder = currentorder;
                }

              
              
                ui._cardRowLayoutEnemy.UpdateCard();
            });
        }
        public  void Set(int slot, bool isme=true )
        {
            try
            {
                UIMainField ui = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
                if (ui == null) return;

                Transform startParent = rectTransform.parent;

                // Đưa lên layer cao nhất để không bị che
                Canvas canvas = GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.overrideSorting = true;
                    canvas.sortingOrder = 999;
                }

                // Bay lên giữa màn
                rectTransform.SetParent(ui._pointActive);
                rectTransform.SetAsLastSibling();

                rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.DOAnchorPos(Vector2.zero, 0.35f).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    rectTransform.DOScale(1.5f, 0.2f).OnComplete(() =>
                    {
                        Transform startParent = isme==true ? ui.TrapZoneMe[slot] : ui.TrapZoneOther[slot];
                        rectTransform.SetParent(startParent);
                        rectTransform.SetAsLastSibling();
                        Sequence seq = DOTween.Sequence();
                        seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.25f).SetEase(Ease.InQuad));
                        seq.Append(rectTransform.DOScale(0.2f, 0.15f).SetEase(Ease.InQuad));
                        seq.OnComplete(() =>
                        {
                            CardIntance cardIntance=isme==true? ui.CardTrapZoneMe[slot]:ui.CardTrapZoneOther[slot];
                            cardIntance._BackGround.gameObject.SetActive(true);
                            cardIntance._frotCard.gameObject.SetActive(false);
                            cardIntance.Card = Card;
                            cardIntance.InstanceId = InstanceId;
                            cardIntance.CardId = CardId;
                            cardIntance.OwnerPlayerId = OwnerPlayerId;
                            cardIntance.ControllerPlayerId = ControllerPlayerId;
                            cardIntance.LoadIcon();
                            cardIntance.gameObject.SetActive(true);
                            Destroy(this.gameObject);
                            if (isme)
                            {
                                ui._cardRowLayoutMe.UpdateCard();

                            }
                            else
                            {
                                ui._cardRowLayoutEnemy.UpdateCard();
                            }
                        });
                    });
                });
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error flipping card: {ex.Message}");
               

            }
        }
        public void UpdateHP(int hp)
        {
            CurrentHp = hp;

            HP.DOKill();

            HP.text = CurrentHp.ToString();

            HP.transform.localScale = Vector3.one;

            Sequence seq = DOTween.Sequence();

            seq.Append(HP.transform.DOScale(1.25f, 0.1f)
                .SetEase(Ease.OutBack));

            seq.Append(HP.transform.DOScale(1f, 0.2f)
                .SetEase(Ease.OutBack));
        }
        public void Setpos(float x, float y)
        {
            localPos = new Vector2 (x, y);
            Debug.Log(localPos);
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
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                GameEvent.Instance.Trigger<ItemSlotBase>(ListEvent.SelectCard.ToString(), this);
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

        public override void SeletecMe(ItemSlotBase itemSlotBase )
        {
            base.SeletecMe(itemSlotBase);
            if(CurrentZone == ZoneType.Hand)
            {
                if (itemSlotBase == this)
                {
                    rectTransform.DOAnchorPos(new Vector2(localPos.x, localPos.y + 50), 0.1f);
                    rectTransform.DOScale(Vector3.one, 0.1f);
                    canvas.sortingOrder = 99;

                    _btnActive.gameObject.SetActive(true);
                    _btnActive.onClick.RemoveAllListeners();
                    TextMeshProUGUI text = _btnActive.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "Triệu Hồi";
                    _btnActive.onClick.AddListener(() =>
                    {
                        CheckTypeSend();

                        _btnActive.gameObject.SetActive(false);
                    });
                    UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
                    if (uIMainField != null)
                    {
                        uIMainField._cardRowLayoutMe.UpdateListCard(this);
                    }
                }
                else
                {
                    _btnActive.gameObject.SetActive(false);
                }
            }
            if (CurrentZone == ZoneType.Monster)
            {
                if (itemSlotBase == this)
                {
                    _btnActive.gameObject.SetActive(true);
                    _btnActive.onClick.RemoveAllListeners();
                    TextMeshProUGUI text = _btnActive.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "Tấn Công";
                    _btnActive.onClick.AddListener(() =>
                    {
                        UIConfirm uIConfirm = UIController.Instance.Get<UIConfirm>(WindowType.UI_Confirm);
                        if (uIConfirm != null)
                        {
                            uIConfirm._idAttacker = (int)InstanceId;
                            uIConfirm.SetConfirmType(ConfirmType.Attatck);
                            uIConfirm.OpenMe();
                        }

                        _btnActive.gameObject.SetActive(false);
                    });
                }
                else
                {
                    _btnActive.gameObject.SetActive(false);
                }

            }
            if (CurrentZone == ZoneType.SpellTrap)
            {
                if (itemSlotBase == this)
                {
                    _btnActive.gameObject.SetActive(true);
                    _btnActive.onClick.RemoveAllListeners();
                    TextMeshProUGUI text = _btnActive.GetComponentInChildren<TextMeshProUGUI>();
                    text.text = "Xem";
                    _btnActive.onClick.AddListener(() =>
                    {


                        _btnActive.gameObject.SetActive(false);
                    });
                }
                else
                {
                    _btnActive.gameObject.SetActive(false);
                }
                    
            }



        }
        private void CheckTypeSend()
        {
            switch (Card._CardType)
            {
                case 1:
                    {
                        ClientMain.Instance.SendNomalSummon((int)InstanceId);
                    }
                    break;
                case 2:
                    {
                    }
                    break;
                case 3:
                    {
                        ClientMain.Instance.SendSetTrap((int)InstanceId);
                    }
                       
                    break;
            }
        }
        private void OnDestroy()
        {
            GameEvent.Instance.Unsubscribe<ItemSlotBase>(ListEvent.SelectCard.ToString(), SeletecMe);
        }
    }
}

