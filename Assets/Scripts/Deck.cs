using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<Card> cards;

    public Deck(List<Card> initialCards)
    {
        cards = new List<Card>(initialCards);
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
}
