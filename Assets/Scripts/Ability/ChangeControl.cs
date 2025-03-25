using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ChangeControlEffEffect")]
public class ChangeControl : Effect
{

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
