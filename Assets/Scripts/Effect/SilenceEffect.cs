using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public enum SilenceTargetType
    {
        Monster,      
        Spell,
        AllOpponentMonsters,
        AllSelfMonsters
    }

    public enum SilenceDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    public class SilenceEffect : ICardEffect
    {
        public SilenceTargetType TargetType = SilenceTargetType.Monster; // Quái, Người chơi, Spell/Trap
        public bool TargetOpponent = true;       // Ngăn đối thủ hay chính mình
        public bool AffectAll = false;           // Áp dụng cho tất cả mục tiêu hợp lệ

        // --- Loại hạn chế ---
        public bool DisableEffects = true;       // Ngăn kích hoạt hoặc áp dụng effect
        public bool DisableActivation = false;   // Ngăn kích hoạt Spell/Trap
        public bool DisableAttack = false;       // Ngăn tấn công (nếu muốn silence mạnh hơn)
        public bool DisableSummon = false;       // Ngăn triệu hồi trong thời gian này

        // --- Bộ lọc ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // --- Thời gian tồn tại ---
        public SilenceDurationType DurationType = SilenceDurationType.UntilEndTurn;
        public int DurationTurns = 1;             // Nếu DurationType = FixedTurns

        // --- Tuỳ chọn khác ---
        public bool Optional = false;             // Người chơi có thể chọn không dùng
        public bool Stackable = false;            // Có cộng dồn với SilenceEffect khác không
        public override void Activite()
        {
            base.Activite();
        }
    }
}