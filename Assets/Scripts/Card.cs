using System;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Monster, Spell }

public  class Card:MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;
    public CardType Type;
    public List<string> Keywords; // Từ khóa bộ bài


    

    public  virtual void ActivateEffect() { }
}
