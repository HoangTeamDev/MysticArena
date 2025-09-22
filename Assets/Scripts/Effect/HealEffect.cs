using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum HealType { Fixed = 0, Percentage = 1 }
    public enum HealTarget
    {
        Self,
        Opponent,
        AllPlayers,
        AllyMonster,
        AllAllies
    }
    public class HealEffect : ICardEffect
    {
        [Header("Basic")]
        public int Amount = 500;
        public HealType HealType = HealType.Fixed;
        public HealTarget Target = HealTarget.Self;

        [Header("Options")]
        [Range(0.1f, 5f)]
        public float Multiplier = 1f;
        public int MaxCap = -1; // -1 = không giới hạn 
        public RequirePhase RequirePhase = RequirePhase.Any;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

