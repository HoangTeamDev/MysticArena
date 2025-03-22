using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bỏ bài vào mộ
[CreateAssetMenu(menuName = "Effects/DiscardCardsEffect")]
public class DiscardCards : Effect
{
    public int number;
    [Header("from")]
    public bool Hand;
    public bool field;
    public bool Deck;
    [Header("to")]
    public bool Graveyard;
    public override void Execute(Card origin, Card target, Player player, GameManager gm)
    {
        try
        {

        }
        catch (Exception ex)
        {
            MainLog.LogError($"Xuất Hiện lỗi  ", ex.ToString(), ReadColor.Yellow);
        }
    }
}