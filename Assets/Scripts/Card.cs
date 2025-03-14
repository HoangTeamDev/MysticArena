using System;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Monster, Spell }

public abstract class Card
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CardType Type { get; private set; }
    public List<string> Keywords { get; private set; } // Từ khóa bộ bài

    public Card(int id, string name, string description, CardType type, List<string> keywords)
    {
        ID = id;
        Name = name;
        Description = description;
        Type = type;
        Keywords = keywords ?? new List<string>();
    }

    public abstract void ActivateEffect();
}
