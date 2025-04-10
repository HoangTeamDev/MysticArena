using Menu.Card;
using Menu.MenuPlay;
using Menu.System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triệu hồi
[CreateAssetMenu(menuName = "Effects/SummonEffect")]
public class SummonEffect : Effect
{
    public int idCard;
    public int amount;
    public int level;
    public int ishighest;
    public KeyWords keyWords;
    public ElementType Element;
    public RaceType Race;
    public bool SummonFormDeck;
    public bool SummonFromHand;
    public bool SummonFromGraveyard;
    public bool isEvolotion;
    public int counturn;
    public override void Execute(Card origin, List<Card> target, Player player, GameManager gm)
    {
        try
        {
            if (counturn > 0) { counturn--; return; }
            if (isEvolotion)
            {
                //thêm hàm xóa quái tiến hóa                   
            }
            else
            {
                //thêm hàm triệu hồi đặc biệt quái

            }
        }
        catch (Exception ex)
        {
            MainLog.LogError($"Xuất Hiện lỗi  ", ex.ToString(), ReadColor.Yellow);
        }
    }
}
