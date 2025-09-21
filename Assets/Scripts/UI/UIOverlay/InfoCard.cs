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

        public override void ShowInfoItem(StringBuilder sb, Item item)
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
