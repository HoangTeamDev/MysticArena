using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    /// <summary>
    /// Destroys a card on the field.
    /// </summary>
    /// 
    public enum DestroyCardType
    {
        Random,
        Targeted
    }

    public enum DestroyOwner
    {
        Self,
        Opponent,
        Both
    }
    public enum ZoneFilter
    {
        Monster,
        SpellTrap,
        All
    }

    public class DestroyCard : ICardEffect
    {
        public int maxDestroy=-1;

        public DestroyCardType destroyCardType=DestroyCardType.Random;
        public DestroyOwner destroyOwner=DestroyOwner.Opponent;
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
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

