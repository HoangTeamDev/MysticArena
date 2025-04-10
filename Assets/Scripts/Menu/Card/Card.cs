using System;
using UnityEngine;

namespace Menu.Card
{
    public enum CardType { Monster, Spell }
    public enum Quality { R, SR, UR, GR }

    [Serializable]
    public class Card:MonoBehaviour
    {
        public int ID;
        public string Name;
        public string Description;
        public CardType Type;
        public KeyWords Keywords;

        public virtual void ActivateEffect() { }
    }
}
