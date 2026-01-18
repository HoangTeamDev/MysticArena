using Card;
using DG.Tweening;
using Menu.System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI.ItemUI;
using UI.SystemUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIOvelay
{
    public class UIInfoCard : UIBase
    {
        [Header("Monter")]
        public List<Sprite> iconQuality;
        public Image quality;
        public List<Sprite> imageBackgroundQuality;
        public Image backgroundQuality;
        public Image imageMonter;
        public List<Sprite> iconElement;
        public TextMeshProUGUI nameMonter;
        public Image element;
        public TextMeshProUGUI level;
        public TextMeshProUGUI ATK;
        public TextMeshProUGUI HP;
        public GameObject montercard;
        public Image backgroundMonter;
        public List<Sprite> bgImage;
        [Header("Spell")]
        public Image imageSpell;
        public TextMeshProUGUI nameSpell;
        public GameObject spellCard;
        [Header("Ability")]
        public TextMeshProUGUI info1;
        public TextMeshProUGUI info2;        
        public RectTransform Sollview;
        public RectTransform main;
        public DesCard desCard;
        public List<DesCard> desCards;
        public void Show(CardData cardData)
        {
            montercard.SetActive(false);
            spellCard.SetActive(false);
            foreach (var item in desCards)
            {
                Destroy(item.gameObject);
            }
            desCards.Clear();
            LoadData(cardData);
            LayoutRebuilder.ForceRebuildLayoutImmediate(Sollview);

        }
      
        public void LoadElement(string name)
        {
            switch (name)
            {
                case "Fire":
                    element.sprite = iconElement[1];
                    break;

                case "Dark":
                    element.sprite = iconElement[0];
                    break;
                case "Wind":
                    element.sprite = iconElement[5];
                    break;
                case "Water":
                    element.sprite = iconElement[4];
                    break;
                case "Earth":
                    element.sprite = iconElement[2];
                    break;
                case "Light":
                    element.sprite = iconElement[3];
                    break;
                case "Thunder":
                    element.sprite = iconElement[6];
                    break;

            }
        }
        public static string ColorWrap(string text, string colorHex) => $"<color={colorHex}>{text}</color>";
        private void LoadData(CardData cardData)
        {
            switch (cardData.cardType)
            {
                case CardType.Monter:
                    {
                        montercard.SetActive(true);
                        spellCard.SetActive(false);
                        nameMonter.text = cardData.nameCard;
                        imageMonter.sprite = Resources.Load<Sprite>("Sprite/Item/" + cardData.id);
                        level.text = cardData.level.ToString();
                        ATK.text = cardData.ATK.ToString();
                        HP.text = cardData.HP.ToString();
                        quality.sprite = iconQuality[(int)cardData.rarity - 1];
                        int x = cardData.rarity is Rarity.GR ? 1:0;
                        
                        backgroundQuality.sprite = imageBackgroundQuality[x];
                        LoadElement(cardData.element.ToString());
                        backgroundMonter.sprite = bgImage[x];
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(ColorWrap($"Tộc: ",ReadColor.Yellow)+$"{cardData.tribe}.");
                        sb.AppendLine(ColorWrap($"Từ Khóa: ",ReadColor.Yellow)+$"{cardData.cardKeyword}.");
                        
                        

                        info1.text = sb.ToString();
                        foreach (var item in cardData.skills)
                        {
                            DesCard des = Instantiate(desCard, Sollview.transform);
                            StringBuilder sb1= new StringBuilder();
                            sb1.AppendLine(ColorWrap($"{item.name}:",ReadColor.Yellow));
                            sb1.AppendLine($"{item.description}");
                            des._des.text = sb1.ToString();
                            desCards.Add(des);
                            des.gameObject.SetActive(true);
                            RectTransform rectTransform = des.GetComponent<RectTransform>();
                            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
                        }
                    }
                    
                    

                    break;
                case CardType.Spell:
                    {
                        spellCard.SetActive(true);
                        montercard.SetActive(false);
                        nameSpell.text = cardData.nameCard;
                        imageSpell.sprite = Resources.Load<Sprite>("Sprite/Item/" + cardData.id);
                        int x = cardData.rarity is Rarity.GR ? 1 : 0;
                        backgroundQuality.sprite = imageBackgroundQuality[x];
                        quality.sprite = iconQuality[(int)cardData.rarity - 1];
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"Tộc: {cardData.tribe}.");
                        sb.AppendLine($"Từ Khóa: {cardData.cardKeyword}.");

                        info1.text = sb.ToString();
                        foreach (var item in cardData.skills)
                        {
                            DesCard des = Instantiate(desCard, Sollview.transform);
                            StringBuilder sb1 = new StringBuilder();
                            sb1.AppendLine(ColorWrap($"{item.name}:", ReadColor.Yellow));
                            sb1.AppendLine($"{item.description}");
                            des._des.text = sb1.ToString();
                            desCards.Add(des);
                            des.gameObject.SetActive(true);
                            RectTransform rectTransform = des.GetComponent<RectTransform>();
                            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
                        }
                    }
                  
                    break;
            }
        }
        public override void Init()
        {
            base.Init();
        }

        public override void Open()
        {
            base.Open();
        }

        public override void OpenMe()
        {
            base.OpenMe();
        }

        public override void Close()
        {
            base.Close();
            gameObject.SetActive(false);
        }

        public override void CloseMe()
        {
            base.CloseMe();
        }

        public override void OnPointerClick(PointerEventData pointerEventData)
        {
            base.OnPointerClick(pointerEventData);
        }

      

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (main != null)
            {
                Vector3 origin = new Vector3(main.localPosition.x, -(Screen.height / 2 + main.sizeDelta.y / 2));

                main.localPosition = new Vector3(main.localPosition.x, -(Screen.height / 2 + main.sizeDelta.y / 2));
                main.DOLocalMoveY(0f, 0.2f).SetEase(Ease.Linear);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

}
