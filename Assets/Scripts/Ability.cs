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
}

public enum AbilityEffectType
{
    Damage,         // Gây sát thương
    Summon,         // Triệu hồi quái
    DrawCard,
    
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
