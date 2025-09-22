using Card;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.UIOvelay;
using UnityEngine;
namespace Effect
{
    public enum StatTargetScope { SelfMonster, ChosenAlly, AllAllies, ChosenOpponent, AllOpponents }
    public enum BuffDurationType { UntilEndTurn, NTurns, Permanent }
    public enum BuffStackPolicy { StackAdd, StackMaxOnly, RefreshDuration, Replace }
    public class BuffAttackEffect : ICardEffect
    {
        [Header("Targeting")]
        public StatTargetScope TargetScope = StatTargetScope.SelfMonster;
        public MonsterCriteria Criteria;               // lọc đối tượng nhận buff (nếu áp cho nhiều/quái chọn       
        public RequirePhase RequirePhase = RequirePhase.Any;

        [Header("Buff")]
        [Min(1)] public int Amount = 300;
        public BuffDurationType Duration = BuffDurationType.UntilEndTurn;
        [Min(1)] public int Turns = 1;                 // chỉ dùng khi Duration = NTurns
        public BuffStackPolicy StackPolicy = BuffStackPolicy.StackAdd;
        public bool Dispellable = true;
        public int MaxCap = 0;                         // 0 = không giới hạn

        [Header("Source (để trace/xoá)")]
        public string SourceId = "";                   // vd: $"{cardId}:{skillId}"
        public override void Activite()
        {
            base.Activite();
        }
    }
    [Serializable]
    public struct MonsterCriteria
    {
        public RequireType Type;
        public RequireRace Race;
        public RequireAttribute Attribute;
        public RequireKeyword Keyword;
        public int MinLevel;  // 0 = bỏ qua
        public int MaxLevel;  // 0 = bỏ qua
        public bool Matches(CardData c)
        {
            if (c == null) return false;
            if (Type != RequireType.Any && !StrEq(c.cardType.ToString(), Type.ToString())) return false;
            if (Race != RequireRace.Any && !StrEq(c.tribe.ToString(), Race.ToString())) return false;
            if (Attribute != RequireAttribute.Any && !StrEq(c.element.ToString(), Attribute.ToString())) return false;
            if (Keyword != RequireKeyword.Any || (MinLevel > 0 || MaxLevel > 0))
            {
                // tuỳ bạn map keyword/level từ CardSpec
            }
            if (MinLevel > 0 && c.level < MinLevel) return false;
            if (MaxLevel > 0 && c.level > MaxLevel) return false;
            return true;
        }
        private static bool StrEq(string a, string b)
           => string.Equals(a?.Trim(), b?.Trim(), StringComparison.OrdinalIgnoreCase);

    }
}

