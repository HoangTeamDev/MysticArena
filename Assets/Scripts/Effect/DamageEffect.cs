using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum DamageTarget
    {
        Opponent,
        Self,
        AllOpponents,
        AllPlayers,
        Monster,
        RandomMonster
    }

    public enum DamageType
    {
        Direct,     // Gây thẳng vào LP
        Battle,     // Sát thương chiến đấu
        Effect,     // Sát thương từ hiệu ứng
        Piercing    // Xuyên DEF
    }
    public class DamageEffect : ICardEffect
    {
        public int Amount = 500;
        public DamageTarget Target = DamageTarget.Opponent;
        public DamageType Type = DamageType.Effect;
        public bool CannotBePrevented = false;
        public RequirePhase RequirePhase = RequirePhase.Any;
        
    }
}
  
