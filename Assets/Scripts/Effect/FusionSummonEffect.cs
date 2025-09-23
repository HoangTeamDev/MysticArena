using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effect
{
    public enum FusionMaterialSource
    {
        Hand = 1 << 0,
        Field = 1 << 1,
        Graveyard = 1 << 2,
        Deck = 1 << 3,
        Banished = 1 << 4
    }

    public enum FusionSummonDurationType
    {
        Instant,     // Xảy ra ngay, không duy trì
        OncePerTurn, // Giới hạn 1 lần / lượt
        Permanent    // Gắn kèm hiệu ứng lâu dài (rất hiếm)
    }
    public class FusionSummonEffect : ICardEffect
    {
        public string FusionMonsterId;                  // ID monster dung hợp cụ thể (nếu cố định)
        public bool AllowAnyFusion = true;              // Cho phép chọn bất kỳ Fusion monster phù hợp
        public int Count = 1;                           // Số monster Fusion có thể gọi ra (thường là 1)

        // --- Nguồn nguyên liệu ---
        public FusionMaterialSource MaterialSources = FusionMaterialSource.Hand | FusionMaterialSource.Field;
        // Hand, Field, Graveyard, Deck, Banished

        // --- Điều kiện nguyên liệu ---
        public List<string> RequiredIds = new();        // Danh sách ID nguyên liệu bắt buộc (nếu có)
        public RequireRace RequireRace = RequireRace.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        public int MinMaterials = 2;                    // Số nguyên liệu tối thiểu
        public int MaxMaterials = 5;                    // Số nguyên liệu tối đa (nếu fusion đặc biệt)

        // --- Tùy chọn ---
        public bool IgnoreSummonCondition = false;      // Bỏ qua điều kiện gốc của monster fusion
        public bool AllowSubstituteMaterials = false;   // Cho phép dùng "substitute" material
        public bool SendToGraveyard = true;             // Nguyên liệu sẽ được gửi về Graveyard
        public bool ShuffleBackToDeck = false;          // Nguyên liệu đưa về Deck thay vì Grave
        public bool BanishMaterials = false;            // Nguyên liệu bị loại bỏ (banish)

        // --- Thời gian / giới hạn ---
        public bool OncePerTurn = true;                 // Giới hạn 1 lần mỗi lượt
        public FusionSummonDurationType DurationType = FusionSummonDurationType.Instant;
        public override void Activite()
        {
            base.Activite();
        }
    }
}