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
     
        [ShowInInspector]public List<CardIntance> Hand = new List<CardIntance>();
        [ShowInInspector] public List<CardIntance> Deck = new List<CardIntance>();
        [ShowInInspector] public List<CardIntance> Graveyard = new List<CardIntance>();
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
