using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Cardrag : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public Transform mparent=null;
    public CardEff cardEff;
    public void OnBeginDrag(PointerEventData eventData)
    {
        mparent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(mparent);
        this.transform.position=mparent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.localScale=Vector3.one;
        cardEff.ActiveOutline(true);
        cardEff.ChangeColor(cardEff.ColorIdle);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

    }
    private void Start()
    {
        cardEff.ActiveOutline(true);
        cardEff.ChangeColor(cardEff.ColorAction);
    }
}
