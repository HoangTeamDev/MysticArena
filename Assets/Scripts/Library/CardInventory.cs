using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardInventory : MonoBehaviour, ISelectHandler
{
    public int idCard;
    public Image image;
    public TextMeshProUGUI textMeshProUGUI;
    public Button ButtonInfo;
    private void Awake()
    {
        ButtonInfo.onClick.AddListener(Show);
    }
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log($"{gameObject.name} đã được SELECT");
    }
    void Show()
    {
        UIController.Instance.ShowCardInfo(idCard);
    }
}
