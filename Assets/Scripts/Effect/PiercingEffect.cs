using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum PiercingType
    {
        None = 0,
        Piercing = 1,
        DoublePiercing = 2
    }
    public enum PiercingDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    public class PiercingEffect : ICardEffect
    {
        public bool isActive = false;
        public PiercingType piercingType = PiercingType.Piercing;
        public PiercingDurationType durationType = PiercingDurationType.UntilEndTurn;
        public int durationTurns = 1; // Used if durationType is FixedTurns
        public bool onlyMe=true; // If true, only the card with this effect gets piercing. If false, all allies get piercing.
        public float numberCardsAffected = 0; // If onlyMe is false, this determines how many allies get the effect. 0 means all allies.
        public RequireType RequireType = RequireType.Any;           // Chỉ áp dụng cho quái loại nào
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

