using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
  
   public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData);
        Cardrag cardrag = eventData.pointerDrag.GetComponent<Cardrag>();
        if (cardrag != null)
        {
            cardrag.mparent = this.transform;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
