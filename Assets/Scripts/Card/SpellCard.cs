using System;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType { Equip, Activation,QuickPlay }

public class SpellCard : Card
{
    public SpellType SpellType { get; private set; }
    public Func<Player, GameBoard, bool> Condition { get; private set; } // Điều kiện kích hoạt
    public Action<Player, GameBoard> Effect { get; private set; } // Hiệu ứng bài phép

   

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
