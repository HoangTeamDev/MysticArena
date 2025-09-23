using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum ProtectTargetType
    {
        Monster,
        Player,
        SpellTrap,
        AllMonsters,
        AllOpponentMonsters,
        AllSelfMonsters
    }

    public enum ProtectDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    public class ProtectEffect : ICardEffect
    {
        public ProtectTargetType TargetType = ProtectTargetType.Monster; // Quái / Người chơi / Toàn bộ field
        public bool AffectAll = false;        // true = bảo vệ tất cả mục tiêu hợp lệ
        public bool TargetOpponent = false;   // Bảo vệ đối thủ hay chính mình

        // --- Các dạng bảo vệ ---
        public bool PreventBattleDestruction = true; // Không bị phá trong chiến đấu
        public bool PreventEffectDestruction = true; // Không bị phá bởi hiệu ứng
        public bool PreventTargeting = false;        // Không thể chọn làm mục tiêu
        public bool PreventDamage = false;           // Không nhận damage (battle/effect)
        public bool RedirectAttack = false;          // Chuyển hướng đòn tấn công sang lá khác

        // --- Bộ lọc áp dụng ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // --- Thời gian tồn tại ---
        public ProtectDurationType DurationType = ProtectDurationType.UntilEndTurn;
        public int DurationTurns = 1;
        public override void Activite()
        {
            base.Activite();
        }
    }

}
