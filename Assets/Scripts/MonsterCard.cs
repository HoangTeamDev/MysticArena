using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum ElementType { Fire, Dark, Wind, Earth, Water, Light, Thunder }
public enum RaceType
{
    Beast, Beast_Warrior, Fairy, Dragon, Fiend, Fish, Machine, Rock, Warrior,
    Zombie, Plant, Dinosaur, Spirit, SpellCaster, Abyss, Insectoid, Demon,
    Titan, Mutant, Behemoth, God
}

public  enum  KeyWords
{
    Evolver, DragonDeity, HolyKnight, Vampire, TyrantDragon, BeastMachine, Jurassic, DivineBlessing, WitchOfDoom, PhantomVeil
    , FlourishingBloom, Necromancy, Tideflow, PrimordialLife,
}

public enum Level
{
    Level1=1, Level2 = 2, Level3 = 3, Level4 = 4, Level5 = 5, Level6 = 6, Level7 = 7, Level8 = 8, Level9 = 9, Level10 = 10,
}
public class MonsterCard : Card
{
    public MonterShow MonterShow;
    public int Level;
    public int LevelOrigin;
    public int ATK;
    public int ATKOrigin;
    public int HP;
    public int HpOrigin;
    public ElementType Element;
    public RaceType Race;
    public List<Ability> Abilities;
    public bool CanAttack;
    public bool HasBeenNormalSummoned;
    
    public void Onload(int id, string name,int level, int atk, int hp, ElementType Element, RaceType Race,List<Ability> Abilities)
    {
        this.Name = name;
        this.Level = level;
        this.ATK = atk;
        this.HP = hp;
        this.HpOrigin = hp;
        this.ATKOrigin=atk;
        this.LevelOrigin = level;
        this.Element = Element;
        this.Race = Race;
        this.Abilities = Abilities;  
        MonterShow.OnShow(id, name,level,atk,hp);
    }
    
}
