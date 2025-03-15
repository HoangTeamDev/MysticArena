using System.Collections.Generic;
using UnityEngine;

public class SummonManager
{
    public static bool TryNormalSummon(Player player, MonsterCard monster)
    {
        if (monster.NormalSummon())
        {
            player.Field.Add(new CardSlot(player.Field.Count));
            Debug.Log($"Triệu hồi thường thành công: {monster.Name}");
            return true;
        }
        return false;
    }

    public static bool TryTributeSummon(Player player, List<MonsterCard> sacrifices, MonsterCard target)
    {
        MonsterCard summonedMonster = MonsterCard.TributeSummon(sacrifices, target);
        if (summonedMonster != null)
        {
            player.Field.Add(new CardSlot(player.Field.Count));
            return true;
        }
        return false;
    }

    /*public static bool TryEvolutionSummon(Player player, MonsterCard monster)
    {
        MonsterCard evolved = monster.Evolve();
        if (evolved != null)
        {
            Debug.Log($"{monster.Name} tiến hóa thành {evolved.Name}!");
            return true;
        }
        return false;
    }*/
}
