using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hấp thụ
[CreateAssetMenu(menuName = "Effects/AbsorbEffect")]
public class Absorb : Effect
{
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
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
