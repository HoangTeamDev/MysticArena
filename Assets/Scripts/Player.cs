using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int HP  = 10000;
    public Deck Deck;
    public List<Card> Hand;
    public List<CardSlot> Field;
    public List<Card> Graveyard;

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
    public bool SummonMonster(MonsterCard monster, List<MonsterCard> sacrifices = null)
    {
        if (monster.Level <= 4)
        {
            return SummonManager.TryNormalSummon(this, monster);
        }
        else if (monster.Level == 5 || monster.Level == 6)
        {
            return SummonManager.TryTributeSummon(this, sacrifices, monster);
        }
       /* else if (monster.CanEvolve)
        {
            return SummonManager.TryEvolutionSummon(this, monster);
        }*/
        return false;
    }
    public bool AddCardToDeck(Card card)
    {
        return Deck.AddCard(card);
    }
}
