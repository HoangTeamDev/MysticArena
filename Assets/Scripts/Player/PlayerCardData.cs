using CardData;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [System.Serializable]
    public class PlayerCardData
    {
       [ShowInInspector] public List<Card> SpellCard { get; set; } = new List<Card> ();
       [ShowInInspector] public List<Card> MonsterCard { get; set; } = new List<Card>();
       [ShowInInspector] public List<Card> TrapCard { get; set; } = new List<Card>();
       
    }
}

