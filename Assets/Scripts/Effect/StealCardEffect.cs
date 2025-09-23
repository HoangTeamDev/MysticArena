using Card;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum StealOwner { Self = 0, Opponent = 1, Chooser = 2 }

    [Flags]
    public enum StealSource
    {
        None = 0,
        Hand = 1 << 0,
        Field = 1 << 1,
        Graveyard = 1 << 2,
        Deck = 1 << 3,
        Extra = 1 << 4,
        Banished = 1 << 5
    }

    public enum StealDestination
    {
        Hand = 0,
        Field = 1,
        TopDeck = 2,
        BottomDeck = 3,
        Graveyard = 4
    }

    public enum StealDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }

    public enum StealReturnDestination
    {
        OriginalOwnerZone,   // trả đúng vùng hợp lệ của chủ gốc
        OriginalOwnerHand,   // trả lên tay
        OriginalOwnerTopDeck // trả lên top deck
    }

    public enum CompensationType
    {
        None,
        Draw,
        Heal
    }

    public enum SummonPosition { Attack = 0, Defense = 1 }

   
    public class StealCardEffect : ICardEffect
    {
        public int Count { get; set; } = 1;                    // Số lá muốn cướp
        public StealOwner Owner = StealOwner.Opponent;         // Cướp từ ai (thường là Opponent)
        public StealSource Sources = StealSource.Hand;         // Lấy ở đâu (flags)
        public StealDestination Destination = StealDestination.Hand; // Đưa về đâu của mình

        // ---- HÀNH VI ----
        public SelectionMode SelectionMode = SelectionMode.PlayerChooses; // Cách chọn lá
        public bool Optional = false;                          // Có thể bỏ qua
        public bool RandomPick = false;                        // Cướp ngẫu nhiên (đặc biệt hữu ích với Hand úp)
        public bool MustReveal = true;                         // Có bắt reveal các lá vừa cướp không
        public bool ShuffleOpponentDeckAfter = true;           // Nếu lấy từ Deck của đối thủ, có xáo lại không
        public bool ExcludeFaceDownOnField = false;            // Không cướp lá đang úp trên sân (nếu luật bạn cấm)
        public bool ExcludeSelf = false;                       // Không cướp chính SourceCard (nếu có thể xảy ra)

        // ---- STEAL TẠM THỜI / HOÀN TRẢ ----
        public bool IsTemporary = false;                       // Cướp tạm
        public StealDurationType DurationType = StealDurationType.UntilEndTurn;
        public int DurationTurns = 1;                          // Nếu FixedTurns
        public bool ReturnToOriginalOwner = true;              // Hết thời gian sẽ trả lại
        public StealReturnDestination ReturnDestination = StealReturnDestination.OriginalOwnerZone; // trả về đâu

        // ---- BỘ LỌC (đồng bộ với hệ Require*) ----
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // Nâng cao
        public string GroupKey = null;                          // lọc theo archetype/group
        public string NameContains = null;                       // tên chứa chuỗi
        public List<string> IdWhitelist = new();
        public List<string> IdBlacklist = new();

        // Range stats (nếu hệ có)
        public int MinLevel = -1, MaxLevel = -1;
        public int MinATK = -1, MaxATK = -1;
        public int MinDEF = -1, MaxDEF = -1;

        // ---- BỒI THƯỜNG / TRADE-OFF (tùy cơ chế) ----
        public bool GrantCompensation = false;                  // Có bồi thường cho đối thủ không
        public CompensationType Compensation = CompensationType.Draw;
        public int CompensationAmount = 1;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

