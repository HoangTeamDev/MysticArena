using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum TokenDurationType { Permanent, UntilEndPhase, TurnsN, UntilLeavesField }
    public enum TokenPlacementRule { AutoFill, ChooseCells, AdjacentToCaster, FrontRowOnly }
    public enum FacePosition { FaceUpAttack, FaceUpDefense, FaceDownDefense } 
    public class SummonTokenEffect : ICardEffect
    {
        [Header("Token Identity & Stats")]
        public string tokenId = "BASIC_TOKEN";
        public int atk = 0;
        public int hp = 0;
        public int level = 1;
        public RequireAttribute attribute = RequireAttribute.Any;
        public RequireRace race = RequireRace.Any;
        public List<RequireKeyword> keywords = new();

        [Tooltip("Copy một phần chỉ số/thuộc tính từ caster (tỉ lệ theo %). 0 = không dùng.")]
        [Range(0f, 200f)] public float inheritAtkPercentFromCaster = 0f;
        [Range(0f, 200f)] public float inheritDefPercentFromCaster = 0f;
        public bool inheritAttributeFromCaster = false;
        public bool inheritRaceFromCaster = false;

        [Header("Quantity & Placement")]
        public int count = 1;
        public DestroyOwner summonOwner = DestroyOwner.Self; // bên nào nhận token
        public TokenPlacementRule placementRule = TokenPlacementRule.AutoFill;
        public ZoneFilter zone = ZoneFilter.Monster;
        public FacePosition face = FacePosition.FaceUpAttack;

        [Header("Lifetime & Despawn")]
        public TokenDurationType durationType = TokenDurationType.Permanent;
        public int durationTurns = 0; // dùng khi TurnsN
        public bool despawnIfNoSpace = true;
        public bool banishOnLeaveField = false; // nếu false -> gửi Grave (hoặc tuỳ hệ thống)

        [Header("Restrictions (Yu-Gi-Oh! style)")]
        public bool cannotBeTributed = true;
        public bool cannotBeUsedAsMaterialFusion = true;      
        public bool cannotBeUsedForCost = false;

        public bool cannotAttack = false;      
        public bool hasSummoningSickness = true; // chờ 1 lượt mới tấn công/đổi tư thế (tùy luật)

        [Header("Limits")]
        public string uniquePerSideKey = "";     // ví dụ: "FLAME_TOKEN"
        public int maxPerSide = -1;              // -1 = không giới hạn
        public int limitPerTurn = 1;

        [Header("Requirements")]
        public RequirePhase requirePhase = RequirePhase.Any;
        public RequireLevel requireLevel = RequireLevel.Any;
        public RequireKeyword requireKeyword = RequireKeyword.Any;


        public override void Activite()
        {
            base.Activite();
        }
    }
}

