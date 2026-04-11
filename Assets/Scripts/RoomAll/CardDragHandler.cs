using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDragHandler : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Vector2 originalPos;
    private Transform originalParent;

    [Header("Drag Settings")]
    [SerializeField] private float dragScale = 1.2f;
    [SerializeField] private float returnDuration = 0.25f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // 👉 Bắt đầu kéo
    public void OnBeginDrag(PointerEventData eventData)
    {
        rect.DOKill();

        originalPos = rect.anchoredPosition;
        originalParent = transform.parent;

        // Đưa lên top UI
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();

        // Scale lên cho nổi bật
        rect.DOScale(dragScale, 0.15f);

        // Cho raycast xuyên qua (để detect drop zone phía dưới)
        canvasGroup.blocksRaycasts = false;
    }

    // 👉 Đang kéo
    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // 👉 Thả
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Nếu không drop vào đâu → quay về
        ReturnToHand();
    }

    // 👉 Gọi khi drop thành công
    public void SnapTo(Transform newParent)
    {
       
        rect.DOKill();

        transform.SetParent(newParent);
        rect.DOScale(0.8f, 0.2f);

        rect.DOAnchorPos(Vector2.zero, 0.2f)
            .SetEase(Ease.OutCubic);
    }

    // 👉 Quay về tay
    private void ReturnToHand()
    {
        transform.SetParent(originalParent);

        rect.DOKill();

        rect.DOScale(0.8f, returnDuration);

        rect.DOAnchorPos(originalPos, returnDuration)
            .SetEase(Ease.OutCubic);
    }
}