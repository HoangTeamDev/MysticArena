using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum HpBuffMode
    {
        HealFlat,              // +N HP ngay lập tức
        HealPercentMax,        // +% MaxHP ngay lập tức
        RegenOverTime,         // Hồi theo lượt: +N mỗi lượt
        AddMaxHpFlat,          // +N MaxHP (tạm thời), có thể hồi phần chênh
        AddMaxHpPercent        // +% MaxHP (tạm thời), có thể hồi phần chênh
    }

    public enum TargetingType
    {
        Self,
        ChooseOne,
        RandomOne,
        AllAllies,
        AllOpponents,
        LowestHpAlly
    }

    public enum StackPolicy
    {
        RefreshDuration,    // làm mới thời lượng, giữ giá trị
        StackValuesUpToCap, // cộng dồn tới trần
        UniqueBySource      // không chồng; nếu cùng nguồn => bỏ qua
    }
    public class BuffHPBuffHPEffect : ICardEffect
    {
        [Header("What this effect does")]
        public HpBuffMode mode = HpBuffMode.HealFlat;

        [Tooltip("Giá trị số (máy) – ví dụ: +300 HP hoặc +30 MaxHP")]
        public int amountFlat = 300;

        [Range(0f, 100f), Tooltip("% theo MaxHP – ví dụ 10 nghĩa là 10% MaxHP")]
        public float amountPercent = 0f;

        [Header("Duration / Over-time")]
        [Tooltip("Số lượt tồn tại với các mode có duration (Regen, AddMaxHP). 0 = tức thời")]
        public int durationTurns = 0;

        [Tooltip("Regen: hồi mỗi lượt giá trị này (nếu 0 sẽ dùng amountFlat/amountPercent)")]
        public int regenPerTurnFlat = 0;

        [Range(0f, 100f)]
        public float regenPerTurnPercent = 0f;

        [Header("MaxHP options")]
        [Tooltip("Khi cộng MaxHP tạm thời, có hồi phần chênh cho current HP không?")]
        public bool healDiffWhenIncreaseMax = true;

        [Tooltip("Khi buff hết hạn, có hạ current HP xuống nếu > MaxHP mới không? (Clamp)")]
        public bool clampOnExpire = true;

        [Header("Stacking & Caps")]
        public StackPolicy stackPolicy = StackPolicy.RefreshDuration;

        [Tooltip("Giới hạn cộng dồn giá trị (áp dụng khi StackValuesUpToCap). -1 = không giới hạn")]
        public int stackCapFlat = -1;

        [Range(-1, 100f), Tooltip("-1 = no cap; nếu >=0 sẽ giới hạn % tổng tối đa")]
        public float stackCapPercent = -1f;

        [Header("Targeting")]
        public TargetingType targeting = TargetingType.Self;
        public DestroyOwner ownerScope = DestroyOwner.Self; // với kiểu 'All...' sẽ dựa scope này

        [Header("Requirements (tuỳ hệ thống của bạn)")]
        public RequirePhase requirePhase = RequirePhase.Any;
        public RequireRace requireRace = RequireRace.Any;
        public RequireAttribute requireAttribute = RequireAttribute.Any;
        public RequireLevel requireLevel = RequireLevel.Any;
        public RequireKeyword requireKeyword = RequireKeyword.Any;
        public override void Activite()
        {
            base.Activite();
        }
    }
}
    
