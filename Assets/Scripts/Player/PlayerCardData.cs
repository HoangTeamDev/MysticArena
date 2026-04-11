using CardData;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Player
{
    [System.Serializable]
    public class PlayerCardData
    {
       [ShowInInspector] public List<Card> SpellCard { get; set; } = new List<Card> ();
       [ShowInInspector] public List<Card> MonsterCard { get; set; } = new List<Card>();
       [ShowInInspector] public List<Card> TrapCard { get; set; } = new List<Card>();
        public void SortAll()
        {
            SortMonster();
            SortSpell();
            SortTrapCard();
        }
        public void SortMonster()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            MonsterCard = MonsterCard
                .OrderBy(x => rarityOrder.ContainsKey(x._Rarity)
                                ? rarityOrder[x._Rarity]
                                : int.MaxValue)
                .ThenBy(x => x._Level) 
                .ThenBy(x=>x._Name)
                .ToList();

            
        }

        public void SortSpell()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            SpellCard = SpellCard
                .OrderBy(x => rarityOrder.ContainsKey(x._Rarity)
                                ? rarityOrder[x._Rarity]
                                : int.MaxValue)
                .ThenBy(x => x._Name)

                .ToList();

           
        }
        public void SortTrapCard()
        {
            var rarityOrder = new Dictionary<string, int>
    {
        { "GR", 0 },
        { "UR", 1 },
        { "SR", 2 }
    };

            TrapCard = TrapCard
                .OrderBy(x => rarityOrder.ContainsKey(x._Rarity)
                                ? rarityOrder[x._Rarity]
                                : int.MaxValue)
                .ThenBy(x => x._Name)

                .ToList();

           
        }
    }
}

