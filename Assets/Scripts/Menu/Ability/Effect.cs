using Menu.Card;
using Menu.MenuPlay;
using Menu.System;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Execute(Card origin, List<Card> target, Player player, GameManager gm);
}


