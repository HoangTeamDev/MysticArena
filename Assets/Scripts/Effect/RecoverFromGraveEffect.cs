using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum RecoverFromGraveOwner { Self = 0, Opponent = 1 }
    public enum RecoverFromGraveType { Random = 0, Chosen = 1 }
    public enum RecoverFromGraveZone { Graveyard = 0, Banished = 1, Both = 2 }
    
    public sealed class RecoverFromGraveEffect : ICardEffect
    {
        public int recoverAmount;
        public RecoverFromGraveOwner Owner = RecoverFromGraveOwner.Self;
        public RecoverFromGraveType Type = RecoverFromGraveType.Random;
        public RecoverFromGraveZone Zone = RecoverFromGraveZone.Graveyard;
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

