using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Execute(Card origin, List<Card> target, Player player, GameManager gm);
}


