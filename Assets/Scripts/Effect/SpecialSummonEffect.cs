using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum SummonSource { Hand, Deck, Graveyard, Banished }  
    public enum SummonOwner { Self, Opponent }
   
    public sealed class SpecialSummonEffect : ICardEffect
    {
        public int Amount;
        public SummonSource Source= SummonSource.Hand;
        public SummonOwner Owner= SummonOwner.Self;
        public RequireType Type = RequireType.Any;
        public RequireRace requireRace = RequireRace.Any;
        public RequireAttribute requireAttribute = RequireAttribute.Any;
        public RequireLevel requireLevel = RequireLevel.Any;
        public RequireKeyword requireKeyword = RequireKeyword.Any;
        public bool summonMe = false;       
        public override void Activite()
        {
            base.Activite();
        }
    }

}

