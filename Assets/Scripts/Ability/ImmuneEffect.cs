using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Miễn hiệu ứng
[CreateAssetMenu(menuName = "Effects/ImmuneEffect")]
public class ImmuneEffect : Effect
{
    public bool SpellEff;
    public bool MonterEff;
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
