using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card
{
    public enum DiscardOwner { Self = 0, Opponent = 1 }
    public enum DrawOwner { Self = 0, Opponent = 1 }
    public enum DiscardType { Random = 0, Chosen = 1 }
    public enum DrawcardType { Random = 0, Chosen = 1 }
    public enum RequirePhase { Any = 0, Main = 1, Battle = 2, End = 3 }
    public enum RequireType
    {
        Any = 0,
        Monster = 1,
        Spell = 2

    }
    public enum RequireRace
    {
        Any = 0,
        Beast = 1,
        BeastWarrior = 2,
        Fairy = 3,
        Dragon = 4,
        Fiend = 5,
        Fish = 6,
        Machine = 7,
        Rock = 8,
        Warrior = 9,
        Zombie = 10,
        Plant = 11,
        Dinosaurs = 12,
        Spellcaster = 13,
        Spirit = 14,
        Abyss = 15,
        Insect = 16,
        Demon = 17,
        Titan = 18,
        Mutant = 19,
        Behemoth = 20,
        God = 21
    }
    public enum RequireAttribute
    {
        Any = 0,
        Fire = 1,
        Water = 2,
        Wind = 3,
        Earth = 4,
        Light = 5,
        Dark = 6
    }
    public enum RequireLevel
    {
        Any = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,

    }
    public enum RequireKeyword
    {
        Any = 0,
        Evolver = 1,
        DragonDeity = 2,
        HolyKnight = 3,
        Vampire = 4,
        TyrantDragon = 5,
        BeastMachine = 6,
        Jurassic = 7,
        DivineBlessing = 8,
        WitchOfDoom = 9,
        PhantomVeil = 10,
        FlourishingBloom = 11,
        Necromancy = 12,
        Tideflow = 13,
        PrimordialLife = 14,
    }
    public enum EffectType
    {
        OnSummon,
        OnDeath,
        Passive,
        OnAttack,
        OnDefend
    }
    /// <summary>
    /// Mã định danh cho các hiệu ứng cơ bản trong game thẻ bài.
    /// Được chia theo nhóm để dễ quản lý.
    /// </summary>
    public enum IdType
    {
        // ==================== Combat & Core (0–9) ====================
        Damage = 0,  // Gây sát thương trực tiếp
        Heal = 1,  // Hồi LP
        DrawCard = 2,  // Rút bài
        DiscardCard = 3,  // Bỏ bài
        DestroyCard = 4,  // Phá hủy bài trên sân
        BuffAttack = 5,  // Tăng ATK
        BuffHP = 6,  // Tăng DEF
        SummonToken = 7,  // Triệu hồi token
        NegateEffect = 8,  // Vô hiệu hiệu ứng
        PreventAttack = 9,  // Ngăn tấn công

        // ==================== Deck / Graveyard (10–19) ====================
        MillCard = 10, // Gửi từ Deck xuống Mộ
        SearchCard = 11, // Tìm bài từ Deck
        RecoverFromGrave = 12, // Lấy từ Mộ về tay
        SpecialSummon = 13, // Triệu hồi đặc biệt
        BanishCard = 14, // Loại bỏ khỏi game
        ReturnToHand = 15, // Đưa bài trên sân về tay
        ReturnToDeck = 16, // Đưa bài về Deck
        ShuffleDeck = 17, // Xáo trộn Deck
        ReviveSelf = 18, // Tự hồi sinh từ mộ
        RevealCard = 19, // Lật/cho đối thủ xem bài

        // ==================== Buff / Debuff (20–29) ====================
        DebuffAttack = 20, // Giảm ATK
        DebuffHP = 21, // Giảm HP
        Piercing = 22, // Damage xuyên thủ
        DoubleAttack = 23, // Tấn công 2 lần
        Protect = 24, // Khiên bảo vệ (không bị phá hủy 1 lần)
        PreventEffect = 25, // Ngăn đối thủ kích hoạt hiệu ứng
        LifeLink = 26, // Hút máu (gây damage → hồi LP)
        BuffAllies = 27, // Buff đồng minh toàn bàn
        DebuffEnemies = 28, // Debuff kẻ địch toàn bàn
        Immunity = 29, // Miễn nhiễm hiệu ứng

        // ==================== Control / Manipulation (30–39) ====================
        ChangeControl = 30, // Đổi quyền kiểm soát quái
        FlipCard = 31, // Lật ngửa úp 1 lá
        LockCard = 32, // Khóa lá không cho dùng/tấn công
        CopyEffect = 33, // Sao chép hiệu ứng
        StealCard = 34, // Ăn cắp bài từ tay/Deck đối thủ
        SealField = 35, // Ngăn đặt/lật bài mới trong X lượt
        Silence = 36, // Vô hiệu kỹ năng quái trong X lượt
       

        // ==================== Summon / Evolution (40–49) ====================
        FusionSummon = 40, // Triệu hồi kết hợp (Fusion)
        Evolution = 45, // Tiến hóa/Upgrade
        Absorb = 46, // Hấp thụ quái khác
        TokenTransform = 47, // Biến token thành quái thật
        FieldSummon = 48, // Triệu hồi dựa theo điều kiện sân
        RandomSummon = 49  // Triệu hồi ngẫu nhiên
    }

    [System.Serializable]
    public abstract class ICardEffect
    {
        public IdType Id;
        public bool RequirePrevSuccess = false;
        public virtual void Activite() { }
    }
}

