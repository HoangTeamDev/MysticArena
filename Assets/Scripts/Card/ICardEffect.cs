using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card
{
    public enum DiscardOwner { Self = 0, Opponent = 1 }
    public enum DiscardType { Random = 0, Chosen = 1 }
    public enum RequirePhase { Any = 0, Main = 1, Battle = 2, End = 3 }
    public enum RequireType
    {
        Any = 0,
        MonsterOnly = 1,
        SpellOnly = 2

    }
    public enum RequireRace
    {
        Any = 0,
        Beast = 1,
        BeastWarrior = 2,
        Fairy = 3,
        Dragon = 4,
        Fiend = 5,
        Fish = 6,
        Machine = 7,
        Rock = 8,
        Warrior = 9,
        Zombie = 10,
        Plant = 11,
        Dinosaurs = 12,
        Spellcaster = 13,
        Spirit = 14,
        Abyss = 15,
        Insect = 16,
        Demon = 17,
        Titan = 18,
        Mutant = 19,
        Behemoth = 20,
        God = 21
    }
    public enum RequireAttribute
    {
        Any = 0,
        Fire = 1,
        Water = 2,
        Wind = 3,
        Earth = 4,
        Light = 5,
        Dark = 6
    }
    public enum RequireLevel
    {
        Any = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,

    }
    public enum RequireKeyword
    {
        Any = 0,
        Evolver = 1,
        DragonDeity = 2,
        HolyKnight = 3,
        Vampire = 4,
        TyrantDragon = 5,
        BeastMachine = 6,
        Jurassic = 7,
        DivineBlessing = 8,
        WitchOfDoom = 9,
        PhantomVeil = 10,
        FlourishingBloom = 11,
        Necromancy = 12,
        Tideflow = 13,
        PrimordialLife = 14,
    }
    public enum EffectType
    {
        OnSummon,
        OnDeath,
        Passive,
        OnAttack,
        OnDefend
    }
    public enum IdType
    {
        Damage = 0,
        Heal = 1,
        DrawCard = 2,
        DiscardCard = 3,
        DestroyCard = 4,
        BuffAttack = 5,
        BuffDefense = 6,
        SummonToken = 7,
        NegateEffect = 8
    }
    [System.Serializable]
    public abstract class ICardEffect
    {
        public IdType Id;     
        public virtual void Activite() { }
    }
}

