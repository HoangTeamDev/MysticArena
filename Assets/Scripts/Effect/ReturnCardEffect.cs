using Card;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    // ====== ENUMS/FLAGS GỢI Ý ======
    public enum ReturnOwner { Self = 0, Opponent = 1, Chooser = 2 }

    [Flags]
    public enum ReturnSource
    {
        None = 0,
        Field = 1 << 0,
        Graveyard = 1 << 1,
        Banished = 1 << 2,
        Hand = 1 << 3,
        Deck = 1 << 4,
        Extra = 1 << 5
    }

    public enum ReturnDestination
    {
        Hand = 0,
        TopDeck = 1,
        BottomDeck = 2,
        Deck = 3,      // trả vào giữa Deck (thường kèm shuffle)
        Extra = 4      // cho các lá Extra-type nếu hệ của bạn có
    }

   
    public class ReturnCardEffect : ICardEffect
    {
        public int Count { get; set; } = 1;                          // Số lá tối đa sẽ trả
        public ReturnOwner Owner = ReturnOwner.Self;                 // Thực hiện trên bài của ai (Self/Opponent/Chooser)
        public ReturnSource Sources = ReturnSource.Field;            // Tìm ở đâu (có thể nhiều nguồn)
        public ReturnDestination Destination = ReturnDestination.Hand; // Trả về đâu

        // Trả cho ai khi Destination là Hand/Deck?
        public bool ToOriginalOwner = true;
        public SelectionMode SelectionMode = SelectionMode.PlayerChooses;
        public bool Optional = false;                                // Có thể bỏ qua
        public bool Random = false;                                  // Ưu tiên random thay vì cho chọn
        public bool MustReveal = true;                               // Khi về Hand có cần reveal không
        public bool ShuffleDeckAfter = true;                         // Nếu về Deck, có xáo không (với DeckShuffle thì auto)
        public bool KeepOrderAsChosen = false;                       // Nếu Top/BottomDeck, giữ thứ tự đã chọn

        // ---- BỘ LỌC (đồng bộ với các effect khác) ----
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;
        public int MinATK = -1, MaxATK = -1;
        public int MinHP = -1, MaxHP = -1;      
        public bool ExcludeSelf = false;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

