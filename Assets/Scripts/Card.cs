using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card:MonoBehaviour
{
    public string CardName;          // Tên lá bài
    public string Description;       // Mô tả
    public int Level;                // Cấp độ (1-6)
    public int Attack;               // Sức tấn công
    public int HP;                   // Máu của quái vật
    public CardType Type;            // Loại bài (Monster, Spell, Trap)
    public ElementType Element;      // Thuộc tính của quái vật
    public MonsterRace Race;         // Tộc của quái vật
    public List<Keyword> Keywords;   // Từ khóa đặc biệt (thuộc bộ bài nào đó)
    public List<Skill> Skills;       // Danh sách kỹ năng của quái
    public Sprite CardImage;         // Ảnh đại diện lá bài

    public int StackCount = 0;       // Dùng cho cơ chế tiến hóa stack
    public bool CanAttack = true;    // Dùng cho cơ chế hấp thụ (hấp thụ xong thì không tấn công)

    public bool IsPhantomVeil => Keywords.Contains(Keyword.PhantomVeil); // Kiểm tra bộ bài Phantom Veil

    public Card(string name, string description, int level, int attack, int hp, CardType type, ElementType element, MonsterRace race, List<Keyword> keywords, List<Skill> skills, Sprite image)
    {
        CardName = name;
        Description = description;
        Level = level;
        Attack = attack;
        HP = hp;
        Type = type;
        Element = element;
        Race = race;
        Keywords = keywords;
        Skills = skills;
        CardImage = image;
    }

    // Hiệu ứng triệu hồi quái
    public bool CanNormalSummon()
    {
        return Level <= 4;
    }

    public bool RequiresTribute(out int requiredTributes)
    {
        if (Level == 5) { requiredTributes = 1; return true; }
        if (Level == 6) { requiredTributes = 2; return true; }
        requiredTributes = 0;
        return false;
    }

    // Hệ thống tương khắc nguyên tố
    public float GetElementalMultiplier(ElementType enemyElement)
    {
        if (IsStrongAgainst(enemyElement)) return 1.15f; // +15% sát thương nếu khắc
        if (IsWeakAgainst(enemyElement)) return 0.85f;   // -15% sát thương nếu bị khắc
        return 1.0f;
    }

    public bool IsStrongAgainst(ElementType enemyElement)
    {
        return (Element == ElementType.Fire && enemyElement == ElementType.Wind) ||
               (Element == ElementType.Wind && enemyElement == ElementType.Earth) ||
               (Element == ElementType.Earth && enemyElement == ElementType.Water) ||
               (Element == ElementType.Water && enemyElement == ElementType.Fire) ||
               (Element == ElementType.Thunder && enemyElement == ElementType.Light) ||
               (Element == ElementType.Light && enemyElement == ElementType.Dark) ||
               (Element == ElementType.Dark && enemyElement == ElementType.Thunder);
    }

    public bool IsWeakAgainst(ElementType enemyElement)
    {
        return (Element == ElementType.Wind && enemyElement == ElementType.Fire) ||
               (Element == ElementType.Earth && enemyElement == ElementType.Wind) ||
               (Element == ElementType.Water && enemyElement == ElementType.Earth) ||
               (Element == ElementType.Fire && enemyElement == ElementType.Water) ||
               (Element == ElementType.Light && enemyElement == ElementType.Thunder) ||
               (Element == ElementType.Dark && enemyElement == ElementType.Light) ||
               (Element == ElementType.Thunder && enemyElement == ElementType.Dark);
    }

    // Tấn công một quái khác
    public void AttackMonster(Card target)
    {
        float multiplier = GetElementalMultiplier(target.Element);
        int damage = Mathf.CeilToInt(Attack * multiplier);
        target.HP -= damage;

        Debug.Log($"{CardName} tấn công {target.CardName}, gây {damage} sát thương!");

        if (target.HP <= 0)
        {
            Debug.Log($"{target.CardName} đã bị phá hủy!");
        }
    }

    // Hiệu ứng hấp thụ
    public void Absorb(Card target)
    {
        int absorbAmount = Mathf.CeilToInt(target.Attack * 0.5f);
        Attack += absorbAmount;
        CanAttack = false; // Không thể tấn công sau khi hấp thụ
        Debug.Log($"{CardName} hấp thụ {target.CardName}, tăng {absorbAmount} ATK!");
    }

    // Tiến hóa quái
    public bool CanEvolve()
    {
        return StackCount >= 5; // Nếu đạt 5 stack thì có thể tiến hóa
    }

    public void AddEvolutionStack()
    {
        if (StackCount < 5)
        {
            StackCount++;
            Attack = Mathf.CeilToInt(Attack * 1.15f);
            HP = Mathf.CeilToInt(HP * 1.15f);
            Debug.Log($"{CardName} nhận stack tiến hóa ({StackCount}/5)!");
        }
    }
}

// Enum cho loại bài
public enum CardType
{
    Monster,
    Spell,
    Trap
}

// Enum cho thuộc tính quái vật
public enum ElementType
{
    Fire, Wind, Earth, Water, Thunder, Light, Dark
}

// Enum cho tộc quái
public enum MonsterRace
{
    Beast, Beast_Warrior, Fairy, Dragon, Fiend, Fish, Machine, Rock, Warrior, Zombie, Plant, Dinosaur, Spirit, SpellCaster,
    Abyss, Insectoid, Demon, Titan, Mutant, Behemoth
}

// Enum cho từ khóa bộ bài
public enum Keyword
{
    PhantomVeil,
    DoomTyrant,
    CelestialDragon
}
