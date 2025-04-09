using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using Menu.System;

public class UIAnimatorExtended : MonoBehaviour
{
    [Header("UI Element")]
    public RectTransform uiElement;

    [Header("Animation Settings")]
    public float duration = 0.5f;
    public Ease easeType = Ease.OutBack;

    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Vector2 originalPosition;
    public int x;
    public UnityEvent Callname;
    public StringEvent StringEvent;
    private void Awake()
    {
        Callname.AddListener(Onmove);
        StringEvent.AddListener(DoCall);
        originalScale = uiElement.localScale;
        originalRotation = uiElement.rotation;
        originalPosition = uiElement.anchoredPosition;

        canvasGroup = uiElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = uiElement.gameObject.AddComponent<CanvasGroup>();
    }
    public void DoCall (string mmm)
    {
        MainLog.Log("okene", mmm, ReadColor.PastelBlue);
    }
    public void Onmove()
    {
        switch (x)
        {
            case 0:
                MoveInFromLeft();
                break;
            case 1:
                MoveInFromRight();
                break;
            case 2:
                MoveInFromUP();
                break;
            case 3:
                MoveInFromDown();
                break;
        }
    }
    public void PlayPopIn()
    {
        uiElement.localScale = Vector3.zero;
        canvasGroup.alpha = 0f;

        Sequence s = DOTween.Sequence();
        s.Append(uiElement.DOScale(originalScale * 1.1f, duration * 0.6f).SetEase(Ease.OutBack));
        s.Append(uiElement.DOScale(originalScale, duration * 0.4f));
        s.Join(canvasGroup.DOFade(1f, duration));
    }

    public void PlayPopOut()
    {
        Sequence s = DOTween.Sequence();
        s.Append(uiElement.DOScale(Vector3.zero, duration).SetEase(Ease.InBack));
        s.Join(canvasGroup.DOFade(0f, duration));
    }

    public void MoveInFromLeft(float screenPercent = 0.5f)
    {
        float offsetX = -Screen.width * screenPercent;
        uiElement.anchoredPosition = new Vector2(originalPosition.x + offsetX, originalPosition.y);
        uiElement.DOAnchorPos(originalPosition, duration).SetEase(easeType);
    }
    public void MoveInFromRight(float screenPercent = 0.5f)
    {
        float offsetX = Screen.width * screenPercent;
        uiElement.anchoredPosition = new Vector2(originalPosition.x + offsetX, originalPosition.y);
        uiElement.DOAnchorPos(originalPosition, duration).SetEase(easeType);
    }
    public void MoveInFromDown(float screenPercent = 0.5f)
    {
        float offsetX = -Screen.height * screenPercent;
        uiElement.anchoredPosition = new Vector2(originalPosition.x , originalPosition.y + offsetX);
        uiElement.DOAnchorPos(originalPosition, duration).SetEase(easeType);
    }
    public void MoveInFromUP(float screenPercent = 0.5f)
    {
        float offsetX = Screen.height * screenPercent;
        uiElement.anchoredPosition = new Vector2(originalPosition.x , originalPosition.y + offsetX);
        uiElement.DOAnchorPos(originalPosition, duration).SetEase(easeType);
    }

    public void MoveOutToRight(float offsetX = 500f)
    {
        uiElement.DOAnchorPos(new Vector2(originalPosition.x + offsetX, originalPosition.y), duration).SetEase(easeType);
    }

    public void RotateIn(float angle = 90f)
    {
        uiElement.rotation = Quaternion.Euler(0, 0, angle);
        uiElement.DORotateQuaternion(originalRotation, duration).SetEase(easeType);
    }

    public void FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, duration);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(0f, duration);
    }

    public void ResetToOriginal()
    {
        uiElement.localScale = originalScale;
        uiElement.rotation = originalRotation;
        uiElement.anchoredPosition = originalPosition;
        canvasGroup.alpha = 1f;
    }
}
