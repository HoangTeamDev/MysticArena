using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;
namespace Menu.Card
{
    public class CardInventory : MonoBehaviour, ISelectHandler
    {
        public int idCard;
        public Image image;
        public TextMeshProUGUI textMeshProUGUI;
        public Button ButtonInfo;
        private void Awake()
        {
            //ButtonInfo.onClick.AddListener(Show);
        }
        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log($"{gameObject.name} đã được SELECT");
        }
        private void OnEnable()
        {
            if (image.sprite == null)
            {
                //image.sprite = Resources.Load<Sprite>("Sprite/Image/" + idCard);

            }
        }
    }
}

