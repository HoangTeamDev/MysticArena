using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{   
    public class DiscardEffect : ICardEffect
    {     
        public int Count { get; set; } = 1;
        public DiscardType discardType = DiscardType.Random;
        public DiscardOwner Owner  = DiscardOwner.Self;
        public RequirePhase RequirePhase  = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;
        public int MinHandRequired  = -1;
        

       

        public override void Activite()
        {
            base.Activite();
        }
    }
}

