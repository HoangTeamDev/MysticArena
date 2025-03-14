using System;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { Fire, Dark, Wind, Earth, Water, Light, Thunder }
public enum RaceType
{
    Beast, Beast_Warrior, Fairy, Dragon, Fiend, Fish, Machine, Rock, Warrior,
    Zombie, Plant, Dinosaur, Spirit, SpellCaster, Abyss, Insectoid, Demon,
    Titan, Mutant, Behemoth, God
}

public class MonsterCard : Card
{
    public int Level { get; private set; }
    public int ATK { get; private set; }
    public int HP { get; private set; }
    public ElementType Element { get; private set; }
    public RaceType Race { get; private set; }
    public List<Ability> Abilities { get; private set; }
    public MonsterCard EvolutionTarget { get; private set; }
    public bool CanAttack;

    public MonsterCard(int id, string name, string description, int level, int atk, int hp,
                       ElementType element, RaceType race, List<Ability> abilities, MonsterCard evolutionTarget = null)
        : base(id, name, description, CardType.Monster, new List<string>())
    {
        Level = level;
        ATK = atk;
        HP = hp;
        Element = element;
        Race = race;
        Abilities = abilities ?? new List<Ability>();
        EvolutionTarget = evolutionTarget;
    }

    public void Attack(MonsterCard target)
    {
        if (target == null) return;

        float damageMultiplier = ElementInteraction.GetDamageMultiplier(this.Element, target.Element);
        int finalDamage = Mathf.RoundToInt(ATK * damageMultiplier);

        Debug.Log($"{Name} tấn công {target.Name} gây {finalDamage} sát thương!");
        target.TakeDamage(finalDamage);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"{Name} bị trừ {damage} HP, còn lại {HP} HP");
        if (HP <= 0) DestroyCard();
    }

    private void DestroyCard()
    {
        Debug.Log($"{Name} đã bị tiêu diệt!");
        foreach (var ability in Abilities)
        {
            if (ability.TriggerCondition == TriggerType.OnDestroy)
                ability.Activate();
        }
    }

    public override void ActivateEffect()
    {
        foreach (var ability in Abilities)
        {
            ability.Activate();
        }
    }
}
