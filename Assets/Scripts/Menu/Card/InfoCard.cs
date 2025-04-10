using DG.Tweening;
using Menu.System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Menu.Card
{
    public class InfoCard : MonoBehaviour
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
        public GameObject main;
        private void OnEnable()
        {
            RectTransform rectTransform = main.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector3 origin = new Vector3(rectTransform.localPosition.x, -(Screen.height / 2 + rectTransform.sizeDelta.y / 2));

                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, -(Screen.height / 2 + rectTransform.sizeDelta.y / 2));
                rectTransform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.Linear);
            }
        }
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
    }

}
