using System;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Monster, Spell }
[System.Serializable]
public  class Card:MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;
    public CardType Type;
    public KeyWords Keywords;


    

    public  virtual void ActivateEffect() { }
}

public class CardInvenTory
{
    public int ID;
    public int Quantity;
    public int QuantityInDeck;
}
