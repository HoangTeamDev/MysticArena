using DG.Tweening;
using Menu.Card;
using Menu.System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI.SystemUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI.UIOvelay
{
    public class InfoCard : UIBase
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
        public GameObject ability;
        public GameObject Sollview;
        public RectTransform main;
       
        public void Show(int id)
        {
            montercard.SetActive(false);
            spellCard.SetActive(false);
            for (int i = 0; i < Sollview.transform.childCount; i++)
            {
                if (i != 0)
                {
                    Destroy(Sollview.transform.GetChild(i).gameObject);

                }
            }
            CardData cardData = Resources.Load<CardData>("Data/" + id);
            if (cardData != null)
            {
                switch (cardData.IsSpell)
                {
                    case true:
                        ShowSpell(id, cardData);
                        break;
                    case false:
                        ShowMonter(id, cardData);
                        break;
                }
            }
        }
        public void ShowMonter(int id, CardData cardData)
        {

            if (cardData != null)
            {
                imageMonter.sprite = Resources.Load<Sprite>("Sprite/Image/" + id);
                if (cardData.Race == RaceType.God)
                {
                    backgroundMonter.sprite = bgImage[0];
                    Color newColor;
                    if (ColorUtility.TryParseHtmlString(ReadColor.monterSP, out newColor))
                    {
                        backgroundMonter.color = newColor;
                    }
                    quality.sprite = iconQuality[3];
                    backgroundQuality.sprite = imageBackgroundQuality[1];
                }
                else
                {
                    backgroundMonter.sprite = bgImage[1];
                    Color newColor;
                    if (ColorUtility.TryParseHtmlString(ReadColor.monter, out newColor))
                    {
                        backgroundMonter.color = newColor;
                    }
                    quality.sprite = iconQuality[2];
                    backgroundQuality.sprite = imageBackgroundQuality[0];

                }
                LoadElement(cardData.Element.ToString());
                nameMonter.text = cardData.Name;
                level.text = ((int)cardData.Level).ToString();
                ATK.text = cardData.ATK.ToString();
                HP.text = cardData.HP.ToString();
                info1.text = $"<color={ReadColor.Yellow}>Tộc</color>\n{cardData.Race}-{cardData.Keywords}";
                info2.text = $"<color={ReadColor.Yellow}>Giới hạn</color>\nTrong bộ bài tối đa 3 lá.";
                foreach (var data in cardData.Abilities)
                {

                    GameObject newo = Instantiate(ability, Sollview.transform);
                    TextMeshProUGUI textMeshProUGUI = newo.GetComponentInChildren<TextMeshProUGUI>();
                    if (textMeshProUGUI != null)
                    {
                        textMeshProUGUI.text = $"<color={ReadColor.Yellow}>{data.AbilityName}:</color>\n{data.Description}";
                    }
                    newo.SetActive(true);
                }
                Sollview.transform.GetChild(0).gameObject.SetActive(false);
                montercard.SetActive(true);
            }
        }
        public void ShowSpell(int id, CardData cardData)
        {
            if (cardData != null)
            {
                imageSpell.sprite = Resources.Load<Sprite>("Sprite/Image/" + id);
                nameSpell.text = cardData.Name;
                info1.text = $"<color={ReadColor.Yellow}>Từ khóa</color>\n{cardData.Keywords}";
                info2.text = $"<color={ReadColor.Yellow}>Giới hạn</color>\nTrong bộ bài tối đa 3 lá.";
                foreach (var data in cardData.Abilities)
                {

                    GameObject newo = Instantiate(ability, Sollview.transform);
                    TextMeshProUGUI textMeshProUGUI = newo.GetComponentInChildren<TextMeshProUGUI>();
                    if (textMeshProUGUI != null)
                    {
                        textMeshProUGUI.text = $"<color={ReadColor.Yellow}>{data.AbilityName}:</color>\n{data.Description}";
                    }
                    newo.SetActive(true);
                }

            }
            Sollview.transform.GetChild(0).gameObject.SetActive(false);
            spellCard.SetActive(true);


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

        public override void ShowInfoItem(StringBuilder sb, Progress.Item item)
        {
            base.ShowInfoItem(sb, item);
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
