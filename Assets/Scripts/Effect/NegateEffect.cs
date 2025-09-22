using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum NegateType
    {
        Activation,
        Effect,
        Attack,
        Continuous
    }

    public enum NegateTarget
    {
        Any,
        MonsterOnly,
        SpellTrapOnly,      
    }

    public class NegateEffect : ICardEffect
    {
        [Header("Negate Settings")]
        public NegateType negateType = NegateType.Effect;
        public NegateTarget negateTarget = NegateTarget.Any;
        public DestroyOwner targetOwner = DestroyOwner.Opponent;

        [Tooltip("Có phá hủy card sau khi negate không?")]
        public bool destroyAfterNegate = false;

        [Header("Duration")]
        [Tooltip("0 = chỉ tức thời, >0 = tồn tại X lượt")]
        public int durationTurns = 0;
        public bool untilEndOfTurn = false;

        [Header("Requirements")]
        public RequirePhase requirePhase = RequirePhase.Any;
        public RequireRace requireRace = RequireRace.Any;
        public RequireAttribute requireAttribute = RequireAttribute.Any;
        public RequireLevel requireLevel = RequireLevel.Any;
        public RequireKeyword requireKeyword = RequireKeyword.Any;

        [Header("Limits")]
        public int limitPerTurn = 1;

        public override void Activite()
        {
            base.Activite();
        }
    }
}
   
