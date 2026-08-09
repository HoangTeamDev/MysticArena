using CardData;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomAll;
namespace Assets.Scripts.RoomAll
{
    [Serializable]
    public class PlayerState
    {
        public int PlayerID;
        public string PlayerName;
        public int hp;
        public int level;
     
        [ShowInInspector]public Dictionary<int, Card> Hand = new Dictionary<int, Card>();
        [ShowInInspector] public Dictionary<int, Card> Deck = new Dictionary<int, Card>();
        [ShowInInspector] public Dictionary<int, Card> Graveyard = new Dictionary<int, Card>();
        public int TurnDurationSeconds { get; private set; } = 60;
        
    }
    [Serializable]
    public class Room
    {
        public int RoomID;
        public PlayerState HostPlayer =new PlayerState();
        public PlayerState GuestPlayer =new PlayerState();
        public PlayerState currentPlayer =new PlayerState();
        public int Turn;

    }
}
