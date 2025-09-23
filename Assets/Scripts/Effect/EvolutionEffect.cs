using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public enum EvolutionDurationType
    {
        Instant,      // Tiến hóa ngay
        OncePerTurn,  // Giới hạn 1 lần mỗi lượt
        Permanent     // Cho phép tiến hóa bất kỳ khi nào có thể (ít gặp)
    }
    public class EvolutionEffect : ICardEffect
    {
        public int TargetMonsterId;                // ID quái vật cơ sở cần tiến hóa (nếu cố định)
        public List<int> EvolutionMonsterIds = new(); // Các lựa chọn tiến hóa có thể
        public bool AllowAnyEvolution = false;        // Cho phép tiến hóa thành bất kỳ quái phù hợp trong danh sách

        // --- Điều kiện tiến hóa ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;   // ví dụ: chỉ tiến hóa khi đạt Level X
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        public int MinTurnsOnField = 0;               // Quái phải tồn tại trên sân ít nhất X lượt
        public bool RequireBattle = false;            // Chỉ tiến hóa sau khi tham gia chiến đấu
        public bool RequireKill = false;              // Chỉ tiến hóa khi đã tiêu diệt đối thủ
        public bool RequireCostTribute = false;       // Có cần tribute thêm quái khác không
        public int ExtraCostCards = 0;                // Số lá bài phải discard/tribute thêm

        // --- Tùy chọn xử lý monster gốc ---
        public bool KeepAsMaterial = true;            // Giữ lại monster gốc làm "nguyên liệu" bên dưới
        public bool SendToGrave = false;              // Gửi về mộ khi tiến hóa
        public bool BanishBase = false;               // Loại bỏ vĩnh viễn monster gốc

        // --- Tùy chọn tiến hóa ---
        public bool IgnoreSummonCondition = false;    // Cho phép bỏ qua điều kiện triệu hồi đặc biệt
        public bool CarryOverBuffs = true;            // Buff từ monster gốc có được giữ lại không
        public bool CarryOverEquip = false;           // Trang bị gắn kèm có chuyển qua không

        // --- Giới hạn ---
        public bool OncePerTurn = true;               // Giới hạn tiến hóa 1 lần / lượt
        public EvolutionDurationType DurationType = EvolutionDurationType.Instant;
        public override void Activite()
        {
            base.Activite();
        }
    }
}