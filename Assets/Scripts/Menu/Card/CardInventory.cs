using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UI.SystemUI;
using UI.UIOvelay;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
namespace Menu.Card
{
    public class CardInventory : Card
    {
       
        public Image image;
        public TextMeshProUGUI textMeshProUGUI;

        public override void ActivateEffect()
        {
            base.ActivateEffect();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        }

        public override void OnInit()
        {
            base.OnInit();
            base._button.onClick.AddListener(() =>
            {
                InfoCard infoCard = UIController.Instance.Get<InfoCard>(WindowType.InfoCard);
                if (infoCard != null)
                {
                    infoCard.Show(ID);
                    infoCard.OpenMe();
                }
            });

            
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }
}

