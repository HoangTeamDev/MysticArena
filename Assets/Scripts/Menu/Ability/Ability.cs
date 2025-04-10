using Menu.Card;
using Menu.MenuPlay;
using Menu.System;
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
    OnGraveyard,
    OnHand,
    OnTurnStart,
    OnDtawPhase,
    OnBattlePhaseStart,
    OnTurnEnd,
}
[CreateAssetMenu(menuName = "Abilities/CompositeAbility")]
public  class Ability : ScriptableObject
{
    [TextArea(2, 10)]
    public string AbilityName;
    [TextArea(5, 10)]
    public string Description;
    public TriggerEvent TriggerCondition;   
    public List<Effect> effects;
    public  void Activate(Card origin=null,List<Card> target = null,Player player = null)
    {
        foreach (var effect in effects)
        {
            effect.Execute(origin, target, player, GameManager.Instance);
        }
    }
   

   

    
}
