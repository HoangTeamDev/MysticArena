using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum PreventAttackMode
    {
        SingleTarget,     // khóa 1 hoặc nhiều quái
        Area,             // khóa tất cả quái theo scope
        SkipBattlePhase,  // bỏ qua Battle Phase của 1 bên
        ForbidDirectAttack// cấm Direct Attack (vẫn được đánh vào quái)
    }

    public enum UnitTargeting
    {
        ChooseOne,
        RandomOne,
        AllAllies,
        AllOpponents,
        AllOnBoard,
        HighestATKOpponent,
        LowestATKOpponent,
        HighestHPOpponent,
        LowestHPOpponent
    }
    public class PreventAttackEffect : ICardEffect
    {
        [Header("Behavior")]
        public PreventAttackMode mode = PreventAttackMode.SingleTarget;
        public UnitTargeting unitTargeting = UnitTargeting.AllOpponents;
        public DestroyOwner ownerScope = DestroyOwner.Opponent; // dùng cho Area/SkipBattlePhase

        [Header("Filters (optional)")]
        public RequirePhase requirePhase = RequirePhase.Any;
        public RequireRace requireRace = RequireRace.Any;
        public RequireAttribute requireAttribute = RequireAttribute.Any;
        public RequireLevel requireLevel = RequireLevel.Any;
        public RequireKeyword requireKeyword = RequireKeyword.Any;
        public bool faceUpOnly = false;

        [Header("Duration")]
        [Tooltip("0 = chỉ trong chain/instant; >0 = tồn tại X lượt của chủ thể bị ảnh hưởng")]
        public int durationTurns = 1;
        public bool untilEndOfTurn = false;

        [Header("Extras")]
        [Tooltip("Ngoài khóa Declare Attack, có cấm đổi tư thế chiến đấu không?")]
        public bool alsoLockBattlePositionChange = false;
        [Tooltip("Có cấm phản công/counter-attack (nếu game bạn có) không?")]
        public bool alsoLockCounterAttack = false;

        [Header("Limits")]
        public int limitPerTurn = 1;
        public int maxUnits = -1; // -1 = không giới hạn, >0 = giới hạn số quái bị khóa
        public override void Activite()
        {
            base.Activite();
        }
    }
}

