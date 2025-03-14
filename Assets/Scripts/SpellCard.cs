using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Equip, Activation }

public class SpellCard : Card
{
    public SpellType SpellType { get; private set; }
    public Action<Player, GameBoard> Effect { get; private set; }

    public SpellCard(int id, string name, string description, SpellType spellType, Action<Player, GameBoard> effect)
        : base(id, name, description, CardType.Spell, new List<string>())
    {
        SpellType = spellType;
        Effect = effect;
    }

    public override void ActivateEffect()
    {
        Debug.Log($"Kích hoạt phép {Name}");
        Effect?.Invoke(null, null); // Cần truyền đối tượng Player/GameBoard thực tế khi gọi hàm này
    }
}
