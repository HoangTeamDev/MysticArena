public enum SkillTrigger
{
    OnSummon,         // Khi triệu hồi
    OnAttack,         // Khi tấn công
    OnDefend,         // Khi bị tấn công
    OnDestroy,        // Khi bị tiêu diệt
    OnSpellTrapActivate, // Khi đối thủ kích hoạt Phép/Bẫy
    OnTurnStart,      // Khi bắt đầu lượt
    OnTurnEnd,        // Khi kết thúc lượt
    OnOpponentSummon, // Khi đối thủ triệu hồi
    OnOpponentAttack  // Khi đối thủ tấn công
}
