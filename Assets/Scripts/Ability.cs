using System;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    OnSummonNomal,       // Khi triệu hồi
    OnSummonSpecial,       // Khi triệu hồi
    OnAttack,       // Khi tấn công
    OnAttacked,       // Khi tấn công
    OnDestroyByBattle,      // Khi bị phá hủy
    OnDestroyByEffect,      // Khi bị phá hủy
    OnHand,
    Graveyard,
    Evolve,
    OnSummonOpponent,     // Khi đối thủ triệu hồi quái vật (có thể kích hoạt phản ứng)
    OnSpellActivate,      // Khi một lá bài phép được kích hoạt
    OnEndTurn,            // Kích hoạt vào cuối lượt
    OnStartTurn,          // Kích hoạt vào đầu lượt
    OnDiscard,            // Khi quái vật bị vứt bỏ vào mộ bài từ tay
    OnDraw,
}

public enum AbilityEffectType
{
    Damage,         // Gây sát thương
    Summon,         // Triệu hồi quái
    DrawCard,
    Heal,
    BoostAttack,          // Tăng ATK cho quái vật
    BoostDefense,         // Tăng HP cho quái vật
    ReduceDamageTaken,    // Giảm sát thương nhận vào

    DestroyCards,         // Phá hủy bài đối thủ (Quái vật hoặc bài phép)
    Absorb,               // Hấp thụ quái hoặc bài phép (Cộng chỉ số hoặc hiệu ứng)
    ForceDiscard,         // Ép đối thủ bỏ bài trên tay vào mộ bài
    StealCard,            // Cướp bài đối thủ (Từ tay hoặc trên sân)
    NegateEffect,         // Vô hiệu hóa kỹ năng hoặc bài phép
    SpecialSummon,        // Triệu hồi đặc biệt (Không cần điều kiện)
    Revive,               // Hồi sinh quái vật từ mộ bài
    Immunity,             // Miễn nhiễm với hiệu ứng bài phép/quái vật
    Evolution,
    GainExtraAttack,      // Tăng số lần tấn công mỗi lượt
    PreventDestruction,   // Ngăn quái vật bị phá hủy (Bởi chiến đấu hoặc hiệu ứng)
    ShuffleIntoDeck,      // Xáo trộn bài từ sân/mộ bài vào bộ bài
    Banish,               // Loại bỏ bài khỏi trận đấu (Không vào mộ bài)

}

[CreateAssetMenu(fileName = "NewAbility", menuName = "Card Game/Ability Data")]
public class Ability : ScriptableObject
{
    public string AbilityName;
    [TextArea] public string Description;
    public bool IsSpellAbility; // ✅ Xác định đây là kỹ năng quái hay bài phép
    public TriggerType TriggerCondition;
    public AbilityEffectType EffectType;

    [Header("Thông số Kỹ Năng")]
    public int EffectValue; // Giá trị hiệu ứng (sát thương, hồi máu, v.v.)
    public int TargetLevel; // Dùng cho kỹ năng liên quan đến cấp độ quái
    public float EffectMultiplier; // Hệ số (ví dụ: hồi máu = 50% HP)

    [Header("Điều kiện Kích Hoạt")]
    public bool RequireSpecificRace;
    public RaceType RequiredRace;
    public bool RequireSpecificElement;
    public ElementType RequiredElement;
    public bool RequireKeyword;
    public KeyWords RequiredKeyword;

    [Header("Hành động Kỹ Năng")]
    public bool SummonFromHand;
    public bool SummonFromGraveyard;
    public bool SummonFromDeck;
    public bool DestroyAllLowerLevel;
    public bool DrawCard;

    public void Activate(MonsterCard owner, Player player, GameBoard gameBoard)
    {
        if (IsSpellAbility)
        {
            ActivateSpellAbility(player, gameBoard, owner);
        }
        else
        {
            ActivateMonsterAbility(owner, gameBoard,player);
        }
    }

    private void ActivateMonsterAbility(MonsterCard owner, GameBoard gameBoard,Player player)
    {
        Debug.Log($"Kích hoạt kỹ năng quái vật: {AbilityName}");

        switch (EffectType)
        {
            case AbilityEffectType.Damage:
                DealDamage(owner, gameBoard);
                break;
            case AbilityEffectType.Summon:
                SummonMonster(player, gameBoard);
                break;
           
        }
    }

    private void ActivateSpellAbility(Player player, GameBoard gameBoard,MonsterCard monster)
    {
        Debug.Log($"Kích hoạt bài phép: {AbilityName}");

        switch (EffectType)
        {
            
            case AbilityEffectType.Summon:
                MonsterCard summoned = gameBoard.DrawMonsterFromDeck(player, 4);
                if (summoned != null)
                {
                    player.Field.Add(new CardSlot(player.Field.Count));
                    Debug.Log($"{summoned.Name} được triệu hồi đặc biệt từ {AbilityName}!");
                }
                break;
        }
    }

    private void DealDamage(MonsterCard owner, GameBoard gameBoard)
    {
        foreach (var slot in gameBoard.OpponentField)
        {
            if (slot.IsOccupied && slot.Card is MonsterCard target)
            {
                int damage = EffectValue > 0 ? EffectValue : Mathf.RoundToInt(owner.ATK * EffectMultiplier);
                target.TakeDamage(damage);
                Debug.Log($"{owner.Name} gây {damage} sát thương lên {target.Name}");
            }
        }
    }

    private void HealPlayer(Player player)
    {
        int healAmount = EffectValue > 0 ? EffectValue : Mathf.RoundToInt(player.HP * EffectMultiplier);
        player.HP += healAmount;
        Debug.Log($"Người chơi hồi {healAmount} HP nhờ {AbilityName}!");
    }

    private void SummonMonster(Player player, GameBoard gameBoard)
    {
        MonsterCard target = null;

        if (SummonFromHand)
            target = player.Hand.Find(card => card is MonsterCard monster && monster.Level <= TargetLevel) as MonsterCard;

        if (SummonFromGraveyard)
            target = player.Graveyard.Find(card => card is MonsterCard monster && monster.Level <= TargetLevel) as MonsterCard;

        if (SummonFromDeck)
            target = gameBoard.DrawMonsterFromDeck(player, TargetLevel);

        if (target != null)
        {
            player.Field.Add(new CardSlot(player.Field.Count));
            Debug.Log($"{target.Name} đã được triệu hồi từ {AbilityName}!");
        }
    }

    private void BoostAttack(MonsterCard owner, GameBoard gameBoard)
    {
        /*foreach (var slot in gameBoard.PlayerField)
        {
            if (slot.IsOccupied && slot.Card is MonsterCard target)
            {
                if ((RequireSpecificRace && target.Race == RequiredRace) ||
                    (RequireSpecificElement && target.Element == RequiredElement) ||
                    (RequireKeyword && target.Keywords.Contains(RequiredKeyword)))
                {
                    target.ATK += EffectValue;
                    Debug.Log($"{target.Name} nhận +{EffectValue} ATK từ {AbilityName}");
                }
            }
        }*/
    }

    private void EvolveMonster(MonsterCard owner, GameBoard gameBoard)
    {
        if (owner.CanEvolve)
        {
            owner.Evolve();
            Debug.Log($"{owner.Name} tiến hóa nhờ {AbilityName}!");
        }
    }

    private void DestroyOpponentCards(MonsterCard owner, GameBoard gameBoard)
    {
        foreach (var slot in gameBoard.OpponentField)
        {
            if (slot.IsOccupied && slot.Card is MonsterCard target && target.Level <= TargetLevel)
            {
                target.DestroyCard();
                Debug.Log($"{target.Name} bị phá hủy bởi {AbilityName}");
            }
        }
    }
}
