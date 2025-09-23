using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum DebuffDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }

    public enum DebuffTargetType
    {
        Monster,      
        AllMonsters,
        AllOpponentMonsters,
        AllSelfMonsters
    }
    public class DebuffEffect : ICardEffect
    {
        // --- Chỉ số cơ bản ---
        public int AttackDecrease = 0;          // Giảm ATK      
        public int HpDecrease = 0;              // Giảm HP (nếu game có chỉ số này)
        public bool DecreasePercent = false;   // true = giảm theo phần trăm, false = giảm theo số nguyêns
        // --- Thời gian / Duration ---
        public DebuffDurationType DurationType = DebuffDurationType.UntilEndTurn;
        public int DurationTurns = 1;           // số lượt tồn tại nếu DurationType = FixedTurns

        // --- Đối tượng tác động ---
        public bool TargetOpponent = true;      // Debuff đối thủ hay bản thân
        public DebuffTargetType TargetType = DebuffTargetType.Monster; // quái, người chơi, toàn bộ field…
        public bool AffectAll = false;          // true = áp dụng toàn bộ mục tiêu hợp lệ

        // --- Ràng buộc Filter ---
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
