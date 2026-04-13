using Assets.Scripts.RoomAll;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.SystemUI;
using UI.UIWindow;
using UIScripts.SystemUI;
using UnityEngine;

public class CardRowLayout : MonoBehaviour
{
    [Header("Container")]
    [SerializeField] private RectTransform contentArea;   // Khung chứa bài
    [SerializeField] private RectTransform contentAreaOther;   // Khung chứa bài
    [SerializeField] private bool isEnemy = false; 

    [Header("Card Settings")]
    [SerializeField] private float cardWidth = 120f;      // chiều ngang 1 lá
    [SerializeField] private float spacing = 20f;         // khoảng cách bình thường giữa các lá
    [SerializeField] private float minSpacing = -60f;     // khoảng cách nhỏ nhất khi chồng lên nhau
    [SerializeField] private bool centerAlign = true;     // căn giữa dãy bài

    [Header("Cards")]
    [SerializeField] private List<RectTransform> cards = new List<RectTransform>();

    public async void AddCard(RectTransform newCard)
    {
        if (newCard == null) return;

        newCard.SetParent(contentArea, true);
        cards.Add(newCard);

        await UpdateLayoutSequential(newCard);
    }

    public async Task DrawMultipleCardsSequential(List<RectTransform> newCards)
    {
        foreach (var card in newCards)
        {
            await DrawOneCardSequential(card);
            await Task.Delay(80); 
        }
    }
    public async Task DrawOneCardSequential(RectTransform newCard)
    {
        if (!isEnemy)
        {
            Room room = GameData.Instance.CurrentRoom;
            PlayerState me = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.HostPlayer : room.GuestPlayer;
            me.Deck.RemoveAt(0);
            UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
            if (uIMainField != null)
            {
                uIMainField.UpdateCardDeckMe();
            }
        }
        else
        {
            Room room = GameData.Instance.CurrentRoom;
            PlayerState enemy = room.HostPlayer.PlayerID == GameData.Instance._mainPlayer._playerid ? room.GuestPlayer : room.HostPlayer;
            enemy.Deck.RemoveAt(0);
            UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
            if (uIMainField != null)
            {
                uIMainField.UpdateCardDeckEnemy();
            }
        }
        RectTransform targetArea = isEnemy ? contentAreaOther : contentArea;
        newCard.SetParent(targetArea, true);
        cards.Add(newCard);

        int count = cards.Count;

        float containerWidth = targetArea.rect.width;
        float finalSpacing = spacing;

        if (count > 1)
        {
            float totalWidthNormal = count * cardWidth + (count - 1) * spacing;

            if (totalWidthNormal > containerWidth)
            {
                finalSpacing = (containerWidth - count * cardWidth) / (count - 1);

                if (finalSpacing < minSpacing)
                    finalSpacing = minSpacing;
            }
        }

        float totalWidth = count * cardWidth + (count - 1) * finalSpacing;
       /* float centerX = contentArea.rect.center.x;
        float centerY = contentArea.rect.center.y;*/
        float centerX = targetArea.rect.width/2;
        float centerY = targetArea.rect.height/2 ;

        float startX = centerX - totalWidth / 2f + cardWidth / 2f;

        List<Task> oldTasks = new List<Task>();

        // 👉 Phase 1: bài cũ dịch ra
        for (int i = 0; i < count; i++)
        {
            var card = cards[i];

            float x = startX + i * (cardWidth + finalSpacing);
            float y = centerY;

            card.SetSiblingIndex(i);
            card.DOKill();

            if (card == newCard) continue;

            var t = card.DOAnchorPos(new Vector2(x, y), 0.1f)
                .SetEase(Ease.Linear);

            oldTasks.Add(t.AsyncWaitForCompletion());
        }

        if (oldTasks.Count > 0)
            await Task.WhenAll(oldTasks);

        // 👉 Phase 2: lá mới bay vào
        int index = cards.IndexOf(newCard);

        float targetX = startX + index * (cardWidth + finalSpacing);
        float targetY = centerY;

        // spawn từ deck (hoặc dưới)
        //newCard.anchoredPosition = new Vector2(targetX, targetY - 200f);
        newCard.localScale = Vector3.one * 0.6f;

        newCard.DOKill();

        await DOTween.Sequence()
            .Join(
                newCard.DOAnchorPos(new Vector2(targetX, targetY), 0.1f)
                    .SetEase(Ease.Linear)
            )
            .Join(
                newCard.DOScale(1.1f, 0.15f).OnComplete(() =>
                {
                    newCard.DOScale(0.8f, 0.1f);
                })
            )
            .AsyncWaitForCompletion();
    }
    public void RemoveCard(RectTransform card)
    {
        if (cards.Remove(card))
        {
           
        }
    }

    public void ClearCards()
    {
        cards.Clear();
        
    }
    [ContextMenu("Update Layout")]
    public async Task UpdateLayoutSequential(RectTransform newCard = null)
    {
        int count = cards.Count;
        if (count == 0) return;

        float containerWidth = contentArea.rect.width;
        float finalSpacing = spacing;

        if (count > 1)
        {
            float totalWidthNormal = count * cardWidth + (count - 1) * spacing;

            if (totalWidthNormal > containerWidth)
            {
                finalSpacing = (containerWidth - count * cardWidth) / (count - 1);

                if (finalSpacing < minSpacing)
                    finalSpacing = minSpacing;
            }
        }

        float totalWidth = count * cardWidth + (count - 1) * finalSpacing;

        float centerX = contentArea.rect.width / 2f;
        float centerY = contentArea.rect.height / 2f;

        float startX = centerX - totalWidth / 2f + cardWidth / 2f;

        List<Tween> oldCardTweens = new List<Tween>();

        // Phase 1: di chuyển toàn bộ bài cũ trước
        for (int i = 0; i < count; i++)
        {
            RectTransform card = cards[i];

            float x = startX + i * (cardWidth + finalSpacing);
            float y = centerY;

            card.SetSiblingIndex(i);
            card.DOKill();

            // Nếu là lá mới thì bỏ qua phase 1
            if (card == newCard)
                continue;

            Tween t = card.DOAnchorPos(new Vector2(x, y), 0.2f)
                .SetEase(Ease.OutCubic);

            oldCardTweens.Add(t);
        }

        // Đợi cụm bài cũ chạy xong
        if (oldCardTweens.Count > 0)
        {
            await Task.WhenAll(oldCardTweens.ConvertAll(t => t.AsyncWaitForCompletion()));
        }

        // Phase 2: lá mới bay vào sau
        if (newCard != null)
        {
            int newIndex = cards.IndexOf(newCard);
            float targetX = startX + newIndex * (cardWidth + finalSpacing);
            float targetY = centerY;

            // Ví dụ: cho lá mới xuất hiện từ dưới lên
          //  newCard.anchoredPosition = new Vector2(targetX, targetY - 200f);
           

            newCard.DOKill();
            await newCard.DOAnchorPos(new Vector2(targetX, targetY), 0.25f)
                .SetEase(Ease.OutCubic)
                .AsyncWaitForCompletion();
        }
    }
}