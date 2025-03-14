using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int HP { get; private set; } = 10000;
    public Deck Deck { get; private set; }
    public List<Card> Hand { get; private set; }
    public List<CardSlot> Field { get; private set; }
    public List<Card> Graveyard { get; private set; }

    public Player(Deck deck)
    {
        Deck = deck;
        Hand = new List<Card>();
        Field = new List<CardSlot>();
        Graveyard = new List<Card>();
    }

    public void DrawCard()
    {
        Card drawnCard = Deck.DrawCard();
        if (drawnCard != null) Hand.Add(drawnCard);
    }

    public void PlayCard(Card card)
    {
        Debug.Log($"Người chơi sử dụng: {card.Name}");
        card.ActivateEffect();
    }
}
