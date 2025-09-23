using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum BanishSource
    {
        Hand,
        Deck,
        Field,
        Graveyard
    }
    public enum BanishOwner
    {
        Self,
        Opponent,
        Both
    }
    public enum BanishType
    {
        Random,
        Chosen,
        All
    }
    public enum BanishZone
    {
        MonsterZone,
        SpellZone,
        Any
    }

    public class BanishCardEffect : ICardEffect
    {
        public int Amount;
        public bool ExcludeSelf = false;
        public BanishSource source = BanishSource.Deck;
        public BanishOwner owner = BanishOwner.Self;
        public BanishType type = BanishType.Random;
        public BanishZone zone = BanishZone.Any;
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

