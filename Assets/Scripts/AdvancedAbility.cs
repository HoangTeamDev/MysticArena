using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hệ thống kỹ năng nâng cao
/// </summary>
public class AdvancedAbility
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TriggerType TriggerCondition { get; private set; }
    public Action<MonsterCard, GameBoard> Effect { get; private set; }

    public AdvancedAbility(string name, string description, TriggerType triggerCondition, Action<MonsterCard, GameBoard> effect)
    {
        Name = name;
        Description = description;
        TriggerCondition = triggerCondition;
        Effect = effect;
    }

    public void Activate(MonsterCard owner, GameBoard gameBoard)
    {
        Debug.Log($"Kích hoạt kỹ năng: {Name}");
        Effect?.Invoke(owner, gameBoard);
    }
}
