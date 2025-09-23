using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum SelectionMode
    {
        PlayerChooses = 0, // UI để người chơi chọn
        TopPriority = 1, // máy tự chọn theo tiêu chí ưu tiên (do bạn định nghĩa)
        Random = 2  // chọn ngẫu nhiên
    }
    public class SearchCardEffect : ICardEffect
    {
        public int Count  = 1;
        public SelectionMode selectionMode= SelectionMode.PlayerChooses; // Cách chọn bài
        public bool Optional = false;                        // Có thể bỏ qua (không thực hiện) không
        public bool MustReveal = true;                       // Có bắt buộc reveal cho đối thủ khi lấy lên tay không
        public bool ShuffleDeckAfter = true;                 // Sau khi lấy từ Deck, có xáo lại Deck không
        public bool AllowDuplicates = true;                  // Cho phép chọn trùng ID không (nếu pool có)
        public bool ExcludeSelf = false;                     // Loại trừ chính lá đang kích hoạt (nếu có concept đó)
        public int MinHandRequired = -1;                     // Điều kiện số bài trên tay trước khi cho phép search (giống DrawCardEffect)
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

