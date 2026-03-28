using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    [System.Serializable]
    public class PlayerCardData
    {
       [ShowInInspector] public Dictionary<int, int> AllCard { get; set; } = new Dictionary<int, int>();
    }
}

