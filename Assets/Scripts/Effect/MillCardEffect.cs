using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Effect
{
    public enum MillType
    {
        TopDeck,
        Random
    }
    public enum MillTarget
    {
        Self,
        Opponent
    }
    public class MillCardEffect : ICardEffect
    {
        public int numberOfCards;     // Số lá cần gửi xuống mộ
        public MillTarget millTarget=MillTarget.Self;   // true = mill đối thủ, false = mill bản thân
        public MillType millType=MillType.TopDeck;       // true = mill từ trên cùng, false = mill ngẫu nhiên
        public RequirePhase RequirePhase = RequirePhase.Any;
        public RequireType RequireType = RequireType.Any;
        public RequireRace RequireRace = RequireRace.Any;
        public RequireLevel RequireLevel = RequireLevel.Any;
        public RequireAttribute RequireAttribute = RequireAttribute.Any;
        public RequireKeyword RequireKeyword = RequireKeyword.Any;
        public override void Activite()
        {
            base.Activite();
        }
    }
}

