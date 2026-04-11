using CardData;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.RoomAll
{
    [Serializable]
    public class PlayerState
    {
        public int PlayerID;
        public string PlayerName;
        public int hp;
        [ShowInInspector] public Dictionary<int, int> Deck = new Dictionary<int, int>();
        public List<Card> Hand = new List<Card>();
        public List<Card> Field = new List<Card>();
        public List<Card> Graveyard = new List<Card>();
        public int TurnDurationSeconds { get; private set; } = 60;
    }
    [Serializable]
    public class Room
    {
        public int RoomID;
        public PlayerState HostPlayer =new PlayerState();
        public PlayerState GuestPlayer =new PlayerState();
        public int Turn;

    }
}
