using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card {
    public enum Tribe
    {
        None = 0,
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
    public enum Element
    {
        None = 0,
        Fire = 1,
        Water = 2,    // "Thủy"
        Wind = 3,
        Earth = 4,
        Light = 5,
        Dark = 6
    }
    [Flags]
    public enum CardKeyword
    {
        None = 0,
        Evolver = 1,
        DragonDeity = 2,
        HolyKnight = 3,
        Vampire = 4,
        TyrantDragon = 5,
        BeastMachine = 6,
        Jurassic = 7,
        DivineBlessing =8,
        WitchOfDoom =9,
        PhantomVeil =10,
        FlourishingBloom =11,
        Necromancy =12,
        Tideflow =13,
        PrimordialLife =14,



    }

    public enum CardType
    {
        None = 0,
        Monter = 1,
        Spell = 2      
    }
    public enum Rarity
    {      
        SR = 1,
        UR = 2,
        GR = 3
    }
    [Serializable]
    [CreateAssetMenu(fileName = "New Card", menuName = "Card/Create New Card")]
    public class CardData:ScriptableObject
    {
        public int id;  
        public string nameCard;
        public Tribe tribe=Tribe.None;
        public Element element=Element.None;
        public CardType cardType=CardType.None;
        public Rarity rarity=Rarity.SR;
        public CardKeyword cardKeyword=CardKeyword.None;
        public int ATK=0;
        public int HP=0;
        public int level=1;
        public List<Skill> skills=new List<Skill>();
    }

}

