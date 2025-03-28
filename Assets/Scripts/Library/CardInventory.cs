using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventory : MonoBehaviour
{
    public int idCard;
    public Image image;
    public TextMeshProUGUI textMeshProUGUI;
    public Button ButtonInfo;
    private void Awake()
    {
        ButtonInfo.onClick.AddListener(Show);
    }
    void Show()
    {
        UIController.Instance.ShowCardInfo(idCard);
    }
}
