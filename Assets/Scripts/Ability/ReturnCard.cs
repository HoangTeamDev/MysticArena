using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ReturnCardEffect")]
public class ReturnCard : Effect
{
    public int amount;
    [Header("From")]
    public bool fromfield;
    public bool fromGraveyard;
    public bool fromHand;
    [Header("To")]
    public bool toHand;
    public bool toDeck;
    public override void Execute(Card origin, List<Card> target, Player player, GameManager gm)
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
