using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect {
    public enum ControlDurationType
    {
        UntilEndTurn,
        FixedTurns,
        Permanent
    }
    public class ChangeControl : ICardEffect
    {
        public bool TargetOpponent = true;          // Lấy quái từ đối thủ hay từ chính mình (đảo lại)
        public int Count = 1;                       // Số quái có thể chiếm quyền
        public bool AffectAll = false;              // Nếu true: lấy hết quái hợp lệ

        // --- Kiểu chiếm quyền ---
        public ControlDurationType DurationType = ControlDurationType.UntilEndTurn;
        public int DurationTurns = 1;               // Nếu DurationType = FixedTurns
        public bool Permanent = false;              // Nếu true: chuyển quyền vĩnh viễn

        // --- Ràng buộc chọn mục tiêu ---
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;

        // --- Tùy chọn ---
        public bool Optional = false;               // Người chơi có thể chọn không
        public bool FaceUpOnly = true;              // Chỉ chọn quái đang ngửa
        public bool CannotTribute = true;           // Giống hiệu ứng Mind Control: không được tribute
        public bool CannotAttack = false;           // Có thể thêm hạn chế tấn công
        public bool ReturnToOwner = true;           // Trả lại cho chủ cũ sau khi hết hiệu lực

        public override void Activite()
        {
            base.Activite();
        }
    }
}

