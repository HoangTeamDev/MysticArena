using Assets.Scripts.RoomAll;
using CardData;
using DG.Tweening;
using RoomAll;
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
    [SerializeField] public List<RectTransform> cards = new List<RectTransform>();
    [SerializeField] public List<CardIntance> cardIntances = new List<CardIntance>();
   
    public void UpdateOrder()
    {
        for (int i = 0;i< transform.childCount;i++)
        {
            Transform child = transform.GetChild(i);
            Canvas canvas = child.GetComponent<Canvas>();

            if (canvas != null)
            {
                canvas.overrideSorting = true; // bắt buộc
                canvas.sortingOrder = i+2;
                CardIntance cardIntance= child.GetComponent<CardIntance>();
                cardIntance.currentorder=canvas.sortingOrder;
            }
        }
    }
    public void UpdateListCard(CardIntance cardIntance )
    {
        foreach(var data in cardIntances)
        {
            if(data != cardIntance)
            {
                data.rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                data.canvas.sortingOrder = data.currentorder;
                data.rectTransform.DOAnchorPos(data.localPos, 0.1f);
            }
           
        }
    }
    public async void UpdateCard()
    {
        RectTransform targetArea = isEnemy ? contentAreaOther : contentArea;
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
        float centerX = targetArea.rect.width / 2;
        float centerY = targetArea.rect.height / 2;

        float startX = centerX - totalWidth / 2f + cardWidth / 2f;

        List<Task> oldTasks = new List<Task>();

        // 👉 Phase 1: bài cũ dịch ra
        for (int i = 0; i < count; i++)
        {
            var card = cards[i];
            CardIntance cardIntance = card.gameObject.GetComponent<CardIntance>();
            float x = startX + i * (cardWidth + finalSpacing);
            float y = centerY;
            cardIntance.Setpos(x, y);
            card.SetSiblingIndex(i);
            card.DOKill();

           

            var t = card.DOAnchorPos(new Vector2(x, y), 0.1f)
                .SetEase(Ease.Linear);

            oldTasks.Add(t.AsyncWaitForCompletion());
        }

        if (oldTasks.Count > 0)
            await Task.WhenAll(oldTasks);
        UpdateOrder();
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
        CardIntance cardIntance2=newCard.GetComponent<CardIntance>();
        cardIntances.Add(cardIntance2);
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
            if (card == newCard) continue;
            card.DOKill();

            CardIntance cardIntance = card.gameObject.GetComponent<CardIntance>();
            cardIntance.Setpos(x, y);
            card.SetSiblingIndex(i);

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
        CardIntance cardIntance1 = newCard.gameObject.GetComponent<CardIntance>();
        if(cardIntance1 != null)
        {
            cardIntance1.Setpos(targetX, targetY);

        }
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
        UpdateOrder();
    }
    public void RemoveCard(RectTransform card)
    {
        cards.Remove(card);
    }

    public void ClearCards()
    {
        cards.Clear();
        
    }
    
  
}