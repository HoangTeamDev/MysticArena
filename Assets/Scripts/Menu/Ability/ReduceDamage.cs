﻿using Menu.Card;
using Menu.MenuPlay;
using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ReduceDamageEffect")]
public class ReduceDamage : Effect
{
    public int damageAmount;
    public bool isDamagePercent;
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