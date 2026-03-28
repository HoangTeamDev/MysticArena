using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [System.Serializable]
    public class PlayerDeckCard
    {
        public int DeckCardId;
        public int DeckId;
        [ShowInInspector] public Dictionary<int, int> Cards = new Dictionary<int, int>();

    }
}

