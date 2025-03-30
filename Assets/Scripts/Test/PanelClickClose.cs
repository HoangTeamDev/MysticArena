using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickCloser : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ClosePanel();
    }

    void ClosePanel()
    {
        gameObject.SetActive(false); // hoặc animation, hoặc bất cứ xử lý gì
    }
}