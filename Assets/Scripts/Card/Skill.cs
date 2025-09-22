using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card
{
    public enum SkillActivation
    {
        IGNITION,     // speed 1 – chủ động (Main Phase)
        QUICKPLAY,    // speed 2 – phản ứng/chuỗi
        TRIGGER,      // kích hoạt có điều kiện (OnSummon/OnDestroy/…)
        CONTINUOUS    // thụ động/điều kiện duy trì
    }
    [System.Serializable]
    public class Skill
    {
        public string skillId;
        public string name;
        public SkillActivation activation;
        public int speed;
        public int limitPerTurn;
        public string groupKey;
        [SerializeReference, SubclassSelector]
        public List<ICardEffect> effects = new();
    }
}

