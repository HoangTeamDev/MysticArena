using CardData;
using DG.Tweening;
using Menu.Connet;
using System.Collections.Generic;
using TMPro;
using UI.ItemUI;
using UI.SystemUI;
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
        public async void Init()
        {
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

            // 🚀 Phase 1: bay + phóng to + xoay nhẹ
            seq.Append(rectTransform.DOAnchorPos(Vector2.zero, 0.35f).SetEase(Ease.OutCubic));
            seq.Join(rectTransform.DOScale(1.3f, 0.35f));
            seq.Join(rectTransform.DORotate(new Vector3(0, 0, Random.Range(-10f, 10f)), 0.35f));

            // 💥 Phase 2: “charge” (dừng 1 nhịp cho cảm giác nặng)
            seq.AppendInterval(0.1f);

            // ⚡ Phase 3: lao xuống slot
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
            rectTransform.DOAnchorPos(new Vector2( localPos.x, localPos.y + 50),0.1f);
            rectTransform.DOScale( Vector3.one,0.1f);
            canvas.sortingOrder = 99;
            UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
            if (uIMainField != null)
            {
                uIMainField._cardRowLayoutMe.UpdateListCard(this);
            }
            UIReview uIReview = UIController.Instance.Get<UIReview>(WindowType.UI_Review);
            if (uIReview != null)
            {
                ButtonConfirm item = uIReview.CreateButtonconfirm();
                item._des.text = "Triệu Hồi";
                item.button.onClick.AddListener(() =>
                {
                    ClientMain.Instance.SendNomalSummon((int)InstanceId);
                    uIReview.CloseMe();
                });


                uIReview.OpenMe();
            }
        }
    }
}

