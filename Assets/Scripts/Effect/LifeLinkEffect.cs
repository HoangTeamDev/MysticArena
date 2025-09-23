using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum LifeLinkMode
    {
        Share,      // Chia sẻ (cả hai cùng mất/được LP theo tỉ lệ)
        Redirect,   // Chuyển hướng (nguồn nhận, linked chịu thay)
        Invert      // Đảo ngược (damage thành heal, heal thành damage)
    }

    public enum LifeLinkTargetType
    {
        Monster,
        Player,
        AllMonsters,
        AllPlayers
    }

    public enum LifeLinkDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    public class LifeLinkEffect : ICardEffect
    {
        public LifeLinkMode Mode = LifeLinkMode.Share;   // Cách liên kết (Share / Redirect / Invert)
        public bool LinkDamage = true;                   // Liên kết sát thương
        public bool LinkHeal = true;                     // Liên kết hồi máu
        public float ShareRatio = 1.0f;                  // Tỉ lệ chia sẻ (1.0 = toàn bộ, 0.5 = một nửa)

        // --- Đối tượng liên kết ---
        public LifeLinkTargetType SourceTarget = LifeLinkTargetType.Monster; // Ai nhận damage gốc
        public LifeLinkTargetType LinkedTarget = LifeLinkTargetType.Player;  // Ai sẽ nhận thêm / chia sẻ

        public bool Bidirectional = false;               // Liên kết hai chiều hay một chiều
        public bool AffectOpponent = false;              // Có áp dụng cho đối thủ không

        // --- Điều kiện / Filter ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // --- Thời gian tồn tại ---
        public LifeLinkDurationType DurationType = LifeLinkDurationType.UntilEndTurn;
        public int DurationTurns = 1;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

