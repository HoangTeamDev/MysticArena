using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [System.Serializable]
    public class PlayerDeck 
    {
        public int _deckID;
        public string _deckName;
        public bool _isActive;
        public string formatType;
        [ShowInInspector] public Dictionary<int, int> _card = new Dictionary<int, int>();
    }
}
