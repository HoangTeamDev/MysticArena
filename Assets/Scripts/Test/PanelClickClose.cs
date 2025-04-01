using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelClickCloser : MonoBehaviour, IPointerClickHandler, ISubmitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject)
        {
            Debug.Log("Click vào PANEL");
            ClosePanel();
        }
       
        
    }
    public void OnSubmit(BaseEventData panelClickCloser)
    {
        Debug.Log("Click vào PANEL1");
    }
    void ClosePanel()
    {
        gameObject.SetActive(false); // hoặc animation, hoặc bất cứ xử lý gì
    }
}