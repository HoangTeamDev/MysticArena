using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Player
{
    [Serializable]
    public class PlayerData
    {
        public string _namePlayer;
        public int _level=-1;
        public int _gold = -1;
        public int _diamond = -1;
        public int _playerid = -1;
        public  PlayerCardData _playerCardData= new  PlayerCardData();
        public List<PlayerDeck> _playerDecks = new List<PlayerDeck>();
        public PlayerDeckCard _playerDeckCard = new PlayerDeckCard();
    }
}

