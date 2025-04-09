using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/DamageEffect")]
public class DamageEffect : Effect
{
    public int damageAmount;//dame
    public bool isDamagePerCent;//gây dame phần trăm
    public bool isMe;//lấy phần trăm atk của mình hay của ai
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

