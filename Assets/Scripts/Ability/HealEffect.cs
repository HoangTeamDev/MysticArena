using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/HealEffect")]
public class HealEffect : Effect
{
    public int healAmount;
    public bool isHealPercent;
    public bool HealforMonter;
    public bool IsHealbyAtkMe;
    public bool IsHealbyHpMe;
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

