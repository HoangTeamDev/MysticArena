using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CardData
{
    [Serializable] 
    
    public class Card
    {
        public int _CardId;
        public string _Name = string.Empty;
        public int _Attack;
        public int _Hp;
        public int _CardType;
        public int _Level;
        public string _Rarity = string.Empty;
        public int _Cost;
        public int _Race;
        public int _Element;
        public int _KeyWord;
        public int _quantity;
        public List<CardEffects> CardEffects = new List<CardEffects>();

        public string GetRace()
        {
            string race = string.Empty;
            switch (_Race)
            {
                case 1: return race= "Dragon";
                case 2: return race = "Warrior";
                case 3: return race = "Fiend";
                case 4: return race = "Machine";
                case 5: return race = "Dinosaur";
                case 6: return race = "Fairy";
                case 7: return race = "Beast";
                case 8: return race = "Beast Warrior";
                case 9: return race = "Fish";
                case 10: return race = "Machine";
                case 11: return race = "Rock";
                case 12: return race = "Zombie";
                case 13: return race = "Plant";
                case 14: return race = "Dinosaur";
                case 15: return race = "Spirit";
                case 16: return race = "Abyss";
                case 17: return race = "Insectoid";
                case 18: return race = "Demon";
                case 19: return race = "Titan";
                case 20: return race = "Mutant";
                case 21: return race = "Behemoth";
                case 22: return race = "God";
                case 23: return race = "SpellCaster";
            }
            return race;
        }
        public string GetKeyWord()
        {
            string key=string.Empty;
            switch (_KeyWord)
            {
                case 0: return key = "Nothing";
                case 1: return key = "Evolver";
                case 2: return key = "Dragon Deity";
                case 3: return key = "Holy Knight";
                case 4: return key = "Vampire";
                case 5: return key = "Tyrant Dragon";
                case 6: return key = "Beast Machine";
                case 7: return key = "Jurassic";
                case 8: return key = "Divine Blessing";
                case 9: return key = "Witch of Doom";
                case 10: return key = "Phantom Veil";
                case 11: return key = "Flourishing Bloom";
                case 12: return key = "Necromancy";
                case 13: return key = "Tideflow";
                case 14: return key = "Primordial Life";
            }
            return key;
        }

        public int GetRate()
        {
            int rate = 0;
            switch (_Rarity)
            {
                case "SR": return rate = 0;
                case "UR": return rate = 1;
                case "GR": return rate = 2;
                
            }
            return rate;
        }

    }
}
