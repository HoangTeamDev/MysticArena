using UnityEngine;
namespace RoomAll
{
    public enum ZoneType
    {
        Deck,
        Hand,
        Monster,
        SpellTrap,
        Graveyard,
        Banished
    }
    public enum PhaseType
    {
        Start,
        Draw,
        Main,
        End
    }
    public class CardIntance
    {
        public long InstanceId { get; set; }
        public int CardId { get; set; }

        public int OwnerPlayerId { get; set; }
        public int ControllerPlayerId { get; set; }

        public ZoneType CurrentZone { get; set; }
        public int SlotIndex { get; set; }

        public bool IsFaceUp { get; set; }
        public int CurrentAtk { get; set; }
        public int CurrentHp { get; set; }
    }
}

