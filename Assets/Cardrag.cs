using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Cardrag : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public Transform mparent=null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        mparent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(mparent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

    }
}
