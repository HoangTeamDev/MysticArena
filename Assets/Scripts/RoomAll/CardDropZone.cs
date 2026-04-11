using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardDragHandler card = eventData.pointerDrag?.GetComponent<CardDragHandler>();

        if (card != null)
        {
            card.SnapTo(transform);

            Debug.Log("Drop thành công!");
        }
    }
}