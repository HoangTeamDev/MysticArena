using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/DestroyCardEffect")]
public class DestroyCard : Effect
{
    public bool isMe;
    public int number;
    public bool isSpell;
    public int level;
    public int atk;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public override void Execute(Card origin, List<Card> target, Player player, GameManager gm)
    {
        try
        {
            if (isMe)
            {

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            MainLog.LogError($"Xuất Hiện lỗi  ", ex.ToString(), ReadColor.Yellow);
        }
    }
}
