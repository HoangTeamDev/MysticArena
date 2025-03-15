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
    public MonsterCard EvolutionTarget; 
    public bool CanAttack;
    public bool HasBeenNormalSummoned;
    public bool CanEvolve => EvolutionTarget != null; // Kiểm tra quái có thể tiến hóa không
    
    public void Onload(int id, string name,int level, int atk, int hp)
    {
        this.Name = name;
        this.Level = level;
        this.ATK = atk;
        this.HP = hp;
        MonterShow.OnShow(id, name,level,atk,hp);
    }
    // ✅ **Triệu hồi thường**
    public bool NormalSummon()
    {
        if (Level > 4 || HasBeenNormalSummoned) return false;
        HasBeenNormalSummoned = true;
        Debug.Log($"{Name} đã được triệu hồi thường!");
        return true;
    }

    // ✅ **Triệu hồi hiến tế (Tribute Summon)**
    public static MonsterCard TributeSummon(List<MonsterCard> sacrifices, MonsterCard target)
    {
        int requiredTributes = target.Level == 5 ? 1 : 2;
        if (sacrifices.Count != requiredTributes) return null;

        Debug.Log($"{target.Name} được triệu hồi bằng cách hiến tế {sacrifices.Count} quái!");
        return target;
    }

    // ✅ **Tiến hóa (Evolution Summon)**
    public MonsterCard Evolve()
    {
        if (!CanEvolve) return null;
        Debug.Log($"{Name} tiến hóa thành {EvolutionTarget.Name}!");
        return EvolutionTarget;
    }

    // ✅ **Hấp thụ quái để tăng sức mạnh**
    public void Absorb(MonsterCard target)
    {
        if (target == null) return;
        this.ATK += Mathf.RoundToInt(target.ATK * 0.5f);
        this.HP += Mathf.RoundToInt(target.HP * 0.5f);
        Debug.Log($"{Name} hấp thụ {target.Name} và tăng sức mạnh!");
    }

    // ✅ **Hồi sinh từ mộ bài**
    public static MonsterCard ReviveFromGraveyard(MonsterCard target)
    {
        Debug.Log($"{target.Name} đã được hồi sinh từ mộ bài!");
        return target;
    }

    // ✅ **Tấn công một quái**
    public void Attack(MonsterCard target)
    {
        if (target == null || !CanAttack) return;

        float damageMultiplier = ElementInteraction.GetDamageMultiplier(this.Element, target.Element);
        int finalDamage = Mathf.RoundToInt(ATK * damageMultiplier);

        Debug.Log($"{Name} tấn công {target.Name} gây {finalDamage} sát thương!");
        target.TakeDamage(finalDamage);
    }

    // ✅ **Nhận sát thương**
    public void TakeDamage(int damage)
    {
        HP -= damage;
        Debug.Log($"{Name} bị trừ {damage} HP, còn lại {HP} HP");
        if (HP <= 0) DestroyCard();
    }

    // ✅ **Phá hủy quái**
    public void DestroyCard()
    {
        Debug.Log($"{Name} đã bị tiêu diệt!");
        foreach (var ability in Abilities)
        {
            if (ability.TriggerCondition == TriggerType.OnDestroy)
                ability.Activate(this, null, null);
        }
    }

    // ✅ **Kích hoạt kỹ năng của quái**
    public void ActivateEffect()
    {
        foreach (var ability in Abilities)
        {
            ability.Activate(this, null, null);
        }
    }
}
