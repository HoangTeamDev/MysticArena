using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum MultiAttackDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    
    public class MultiAttackEffect : ICardEffect
    {
        public int ExtraAttacks = 1;           // Số lần tấn công thêm (1 = tổng 2 lần / lượt)
        public bool OnlyMonsters = true;       // Chỉ cho phép tấn công quái (không đánh trực tiếp)
        public bool OncePerTurn = true;        // Có giới hạn 1 lần kích hoạt mỗi lượt không

        // --- Bộ lọc áp dụng ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // --- Thời gian tồn tại ---
        public MultiAttackDurationType DurationType = MultiAttackDurationType.UntilEndTurn;
        public int DurationTurns = 1;          // Nếu DurationType = FixedTurns

        // --- Tuỳ chọn khác ---
        public bool CanAttackDirectly = false; // Cho phép đánh thẳng vào LP nếu không có quái
        public bool Stackable = false;         // Có cộng dồn với MultiAttackEffect khác không
        public override void Activite()
        {
            base.Activite();
        }
    }

}
