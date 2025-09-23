using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public enum AbsorbHostSelect { ThisCard, SelectMyMonster, ById }
    public enum AbsorbOwner { Self, Opponent, Chooser }

    [System.Flags]
    public enum AbsorbSource
    {
        None = 0,
        Field = 1 << 0,
        Graveyard = 1 << 1,
        Hand = 1 << 2,
        Deck = 1 << 3,
        Banished = 1 << 4
    }

    public enum AbsorbMode { Attach, Consume }

    public enum AbsorbOnTargetResult
    {
        SendToUnderlay, // làm material/overlay dưới host
        SendToGrave,
        Banish,
        Destroy
    }

    public enum AbsorbCopyDuration
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }

   

    public enum StatType { ATK, HP, All }
    public class AbsorbEffect : ICardEffect
    {
        public AbsorbHostSelect HostSelect = AbsorbHostSelect.ThisCard; // Quái chủ là lá đang kích hoạt hay do người chơi chọn
        public string HostMonsterId = null;                              // Nếu cho phép chọn host theo ID cụ thể

        // --- Đối tượng bị hấp thụ ---
        public AbsorbOwner TargetOwner = AbsorbOwner.Opponent;           // Hấp thụ bài của ai
        public AbsorbSource Sources = AbsorbSource.Field;                // Khu vực lấy mục tiêu (flags)
        public int MinTargets = 1;
        public int MaxTargets = 1;
        public SelectionMode SelectionMode = SelectionMode.PlayerChooses;
        public bool Optional = false;
        public bool ExcludeSelf = true;                                  // Không cho tự hấp thụ chính mình

        // --- Kiểu hấp thụ ---
        public AbsorbMode Mode = AbsorbMode.Attach;                      // Attach (làm material) | Consume (tiêu thụ)
        public AbsorbOnTargetResult OnTargetResult = AbsorbOnTargetResult.SendToUnderlay; // Kết quả xử lý mục tiêu
        public bool DetachOnHostLeaves = true;                           // Nếu host rời sân, material xử lý như thế nào (theo luật game bạn)

        // --- Hiệu ứng khi Consume ---
        public bool GainStats = true;            // Nhận chỉ số từ mục tiêu
        public float GainATKRatio = 1.0f;        // % ATK của mục tiêu chuyển sang host
        public float GainDEFRatio = 0.0f;        // % DEF của mục tiêu chuyển sang host
        public float GainHPRatio = 0.0f;        // % HP của mục tiêu chuyển sang host (nếu có)
        public bool CopyKeywords = false;        // Sao chép keyword từ mục tiêu
        public bool CopyEffects = false;        // Sao chép kỹ năng/eff của mục tiêu
        public AbsorbCopyDuration CopyDuration = AbsorbCopyDuration.UntilEndTurn;
        public int CopyDurationTurns = 1;

        // --- Bồi dưỡng/bốc/máu khi hấp thụ (tùy gameplay) ---
        public bool HealOwnerOnAbsorb = false;
        public int HealAmountPerTarget = 0;
        public bool DrawOnAbsorb = false;
        public int DrawPerTarget = 0;

        // --- Bộ lọc mục tiêu (đồng bộ với hệ Require*) ---
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        public string GroupKey = null;
        public string NameContains = null;
        public List<string> IdWhitelist = new();
        public List<string> IdBlacklist = new();
        public int MinLevel = -1, MaxLevel = -1;
        public int MinATK = -1, MaxATK = -1;
        public int MinDEF = -1, MaxDEF = -1;

        // --- Giới hạn & chồng lấp ---
        public bool OncePerTurn = false;
        public bool Stackable = true;
        public override void Activite()
        {
            base.Activite();
        }
    }
}