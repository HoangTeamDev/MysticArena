using System;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void Execute(Card origin, Card target, Player player, GameManager gm);
}


