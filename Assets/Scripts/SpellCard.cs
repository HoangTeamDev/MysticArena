using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Equip, Activation }

public class SpellCard : Card
{
    public SpellType SpellType { get; private set; }
    public AbilityEffectType EffectType { get; private set; } // Hiệu ứng phép
    public Func<Player, GameBoard, bool> Condition { get; private set; } // Điều kiện kích hoạt
    public Action<Player, GameBoard> Effect { get; private set; } // Hiệu ứng bài phép

    public SpellCard(int id, string name, string description, SpellType spellType, AbilityEffectType effectType,
                     Func<Player, GameBoard, bool> condition, Action<Player, GameBoard> effect)
        : base(id, name, description, CardType.Spell, new List<string>())
    {
        SpellType = spellType;
        EffectType = effectType;
        Condition = condition ?? ((player, board) => true); // Nếu không có điều kiện, mặc định luôn đúng
        Effect = effect;
    }

    public override void ActivateEffect()
    {
        if (Condition?.Invoke(null, null) ?? false) // Kiểm tra điều kiện trước khi kích hoạt
        {
            Debug.Log($"Kích hoạt phép {Name}");
            Effect?.Invoke(null, null);
        }
        else
        {
            Debug.Log($"Không thể kích hoạt {Name}, điều kiện chưa đạt!");
        }
    }
}
