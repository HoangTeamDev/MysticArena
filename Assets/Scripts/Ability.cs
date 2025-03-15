using System;
using UnityEngine;

public enum TriggerType
{
    OnSummon,       // Khi triệu hồi
    OnAttack,       // Khi tấn công
    OnDestroy,      // Khi bị phá hủy
    OnSpellActivate // Khi kích hoạt bài phép
}

public enum AbilityEffectType
{
    Damage,         // Gây sát thương
    Summon,         // Triệu hồi quái
    DrawCard,       // Rút bài
    AddSpellToHand, // Lấy bài phép lên tay
    Heal,           // Hồi máu
    Evolution,      // Tiến hóa
    BoostAttack,    // Tăng ATK
    ReduceDamage,   // Giảm sát thương nhận vào
    DestroyCards,   // Phá hủy bài đối thủ
    Absorb,         // Hấp thụ quái hoặc phép
    ForceDiscard,   // Bắt đối thủ bỏ bài
    DoubleAttack,   // Tấn công 2 lần
    Immunity        // Miễn nhiễm với hiệu ứng
}
public enum SpellEffectType
{
    Heal,             // Hồi máu
    Evolve,           // Tiến hóa quái
    SpecialSummon,    // Triệu hồi đặc biệt
    Revive,           // Hồi sinh quái vật
    GrantAbility,     // Cấp kỹ năng cho quái
    Equip,            // Trang bị kỹ năng hoặc hiệu ứng
    DestroyOpponent,  // Phá hủy bài đối thủ
    Damage,           // Gây sát thương
}
[CreateAssetMenu(fileName = "NewAbility", menuName = "Card Game/Ability Data")]
public class Ability: ScriptableObject
{
    public string Name;
    public string Description;
    public TriggerType TriggerCondition;
    public SpellEffectType EffectType;
    public Func<bool> Condition;
    public Action<MonsterCard, Player, GameBoard> Effect;

    public Ability(string name, string description, TriggerType triggerCondition,
                   Func<bool> condition, Action<MonsterCard, Player, GameBoard> effect)
    {
        Name = name;
        Description = description;
        TriggerCondition = triggerCondition;
        Condition = condition ?? (() => true); 
        Effect = effect;
    }

    public void Activate(MonsterCard owner, Player player, GameBoard gameBoard)
    {
        if (Condition.Invoke()) 
        {
            Debug.Log($"Kích hoạt kỹ năng: {Name}");
            Effect?.Invoke(owner, player, gameBoard);
        }
        else
        {
            Debug.Log($"Không thể kích hoạt kỹ năng: {Name}, điều kiện chưa đạt!");
        }
    }
}
