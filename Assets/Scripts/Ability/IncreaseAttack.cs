using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/IncreaseAttackEffect")]
public class IncreaseAttack : Effect
{
    public int amount;
    public int atk;
    public bool percent;
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
