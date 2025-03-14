using System;
using UnityEngine;

public enum TriggerType
{
    OnSummon,
    OnAttack,
    OnDestroy,
    OnSpellActivate
}

public class Ability
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TriggerType TriggerCondition { get; private set; }
    public Action Effect { get; private set; }

    public Ability(string name, string description, TriggerType triggerCondition, Action effect)
    {
        Name = name;
        Description = description;
        TriggerCondition = triggerCondition;
        Effect = effect;
    }

    public void Activate()
    {
        Debug.Log($"Kích hoạt kỹ năng: {Name}");
        Effect?.Invoke();
    }
}
