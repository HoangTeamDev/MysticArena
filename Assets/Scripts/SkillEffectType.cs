public enum SkillEffectType
{
    ReflectDamage,     // Phản sát thương
    DestroyCard,       // Phá hủy bài
    NegateEffect,      // Vô hiệu hóa hiệu ứng
    SpecialSummon,     // Triệu hồi đặc biệt
    IncreaseATK,       // Tăng ATK
    IncreaseHP,        // Tăng HP
    ReduceATK,         // Giảm ATK đối thủ
    ReduceHP,          // Giảm HP đối thủ
    DrawCard,          // Rút bài
    ProtectCard,       // Bảo vệ bài khỏi bị phá hủy
    AbsorbCard,        // Hấp thụ bài
    ChangeControl,     // Chiếm quyền điều khiển quái
    StackEffect,       // Đặt Stack lên bài
    PreventSummon,     // Cấm triệu hồi
    PreventAttack,     // Cấm tấn công
    SwapPosition,      // Đổi thế bài
    Revival,           // Hồi sinh quái
    PointSystem,       // Hệ thống điểm (tích lũy)
    FieldWipe          // Hủy toàn bộ bài trên sân
}
