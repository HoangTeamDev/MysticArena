using System;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerEvent
{
    OnNomalSummon,
    SpecialSummon,
    OnAttacked,
    OnTargetByEff,
    OnDestroy,
    OnAttack,
    OnDestroyByEff,
    OnDestroyByBattle,
    OnEnterGraveyard,
    OnOpponentActivatesEffect,
    OnActivateEffect,
    OnOpponentDraw,
    OnOpponentSummon,
    OnPlayerDraw,
    OnField,
    OnTurnStart,
    OnDtawPhase,
    OnBattlePhaseStart,
    OnTurnEnd,
}
[CreateAssetMenu(menuName = "Abilities/CompositeAbility")]
public  class Ability : ScriptableObject
{
    public string AbilityName;
    public string Description;
    public TriggerEvent TriggerCondition;   
    public List<Effect> effects;
    public  void Activate(Card origin=null,Card target = null,Player player = null)
    {
        foreach (var effect in effects)
        {
            effect.Execute(origin, target, player, GameManager.Instance);
        }
    }
   

   

    
}
