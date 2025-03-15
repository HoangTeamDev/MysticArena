using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    private List<Card> cards;
    private const int MaxCopiesPerCard = 2; // Số lượng tối đa cho mỗi lá bài

    public Deck(List<Card> initialCards)
    {
        cards = new List<Card>();
        foreach (var card in initialCards)
        {
            AddCard(card);
        }
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(0, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0) return null;
        Card topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    // ✅ Thêm lá bài vào bộ bài với kiểm tra số lượng tối đa
    public bool AddCard(Card newCard)
    {
        int count = cards.Count(c => c.ID == newCard.ID);
        if (count >= MaxCopiesPerCard)
        {
            Debug.Log($"Không thể thêm {newCard.Name}, đã đạt giới hạn {MaxCopiesPerCard} lá!");
            return false;
        }
        cards.Add(newCard);
        return true;
    }
}
