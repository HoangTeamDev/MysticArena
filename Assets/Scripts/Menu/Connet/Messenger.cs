using Assets.Scripts.RoomAll;
using CardData;
using Menu.System;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.SystemUI;
using UI.UIOvelay;
using UI.UIWindow;
using UIScripts.SystemUI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Menu.Connet
{
    public class Messenger : MonoBehaviour
    {
        private GameData gameData => GameData.Instance;
        public void Handle(Message message)
        {
            MainLog.Log("Nhận mess", message.Command.ToString(), ReadColor.Green);

            switch (message.Command)
            {
                case 1:
                    {
                        HandleLoginSucces(message);
                        break;
                    }
                case 2:

                    break;
                case 3:
                    HandleCreatePlayer(message);

                    break;
                case 4:
                    {
                        HandleInfo(message);
                    }
                    break;
                case 5:
                    {
                        HandleGetAllCard(message);
                    }
                    break;
                case 6:
                    {
                        HandlePlayerCard(message);
                    }
                    break;
                case 7:
                    {
                        HandlePlayerDeck(message);
                    }
                    break;
                case 8:
                    {
                        HandlePlayerDeckCard(message);
                    }
                    break;
                case 10:
                    {
                        string msg = message.readUTF();
                        Debug.Log(msg);
                    }

                    break;
                case 11:
                    {
                        HandleUpdatePlayerDeckCard(message);
                    }
                    break;
                case 9:
                    {
                        HandleUpdatePlayerCard(message);
                    }
                    break;
                case 12:
                    {
                        HandleGetEffCard(message);
                    }
                    break;
                case 13:
                    {
                        HandleRoom(message);


                    }
                    break;

                default:
                    Debug.LogWarning("Unknown opcode: " + message);
                    break;
            }
        }
        void HandleUpdatePlayerCard(Message message)
        {
            try
            {
                Card card = new Card();
                card._CardId = message.readInt();
                card._quantity = message.readInt();
                card._CardType = message.readByte();
                card._Rarity = gameData.GetRaity(card._CardId);
                card._Name = gameData.GetName(card._CardId);
                if (card._quantity == 0)
                {
                    switch (card._CardType)
                    {
                        case 1:
                            {
                                Card card1 = gameData._mainPlayer._playerCardData.MonsterCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerCardData.MonsterCard.Remove(card1);

                                }
                            }
                            break;
                        case 2:
                            {
                                Card card1 = gameData._mainPlayer._playerCardData.SpellCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerCardData.SpellCard.Remove(card1);

                                }
                            }

                            break;
                        case 3:
                            {
                                Card card1 = gameData._mainPlayer._playerCardData.TrapCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerCardData.TrapCard.Remove(card1);

                                }
                            }

                            break;

                    }

                }
                else
                {
                    switch (card._CardType)
                    {
                        case 1:
                            {
                                card._Level = gameData.level(card._CardId);
                                Card card1 = gameData._mainPlayer._playerCardData.MonsterCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;

                                }
                                else
                                {
                                    gameData._mainPlayer._playerCardData.MonsterCard.Add(card);
                                }
                            }
                            break;
                        case 2:
                            {
                                Card card1 = gameData._mainPlayer._playerCardData.SpellCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;

                                }
                                else
                                {
                                    gameData._mainPlayer._playerCardData.SpellCard.Add(card);
                                }
                            }

                            break;
                        case 3:
                            {
                                Card card1 = gameData._mainPlayer._playerCardData.TrapCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;

                                }
                                else
                                {
                                    gameData._mainPlayer._playerCardData.TrapCard.Add(card);
                                }
                            }

                            break;

                    }

                }
                gameData._mainPlayer._playerCardData.SortAll();
                UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                if (uIDeck != null)
                {
                    uIDeck.CreatePlayerCard();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        void HandleUpdatePlayerDeckCard(Message message)
        {
            try
            {
                Card card = new Card();

                card._CardId = message.readInt();
                card._quantity = message.readInt();
                card._CardType = message.readByte();
                card._Rarity = gameData.GetRaity(card._CardId);
                card._Name=gameData.GetName(card._CardId);
                if (card._quantity == 0)
                {
                    switch (card._CardType)
                    {
                        case 1:
                            {
                                Card card1 = gameData._mainPlayer._playerDeckCard.MonsterCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerDeckCard.MonsterCard.Remove(card1);
                                }
                            }
                            break;
                        case 2:
                            {
                                Card card1 = gameData._mainPlayer._playerDeckCard.SpellCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerDeckCard.SpellCard.Remove(card1);
                                }
                            }
                            break;
                        case 3:
                            {
                                Card card1 = gameData._mainPlayer._playerDeckCard.TrapCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    gameData._mainPlayer._playerDeckCard.TrapCard.Remove(card1);
                                }
                            }
                            break;
                    }
                }
                else
                {

                    switch (card._CardType)
                    {
                        case 1:
                            {
                                card._Level = gameData.level(card._CardId);
                                Card card1 = gameData._mainPlayer._playerDeckCard.MonsterCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;
                                }
                                else
                                {
                                    gameData._mainPlayer._playerDeckCard.MonsterCard.Add(card);
                                }
                            }

                            break;
                        case 2:
                            {
                                Card card1 = gameData._mainPlayer._playerDeckCard.SpellCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;
                                }
                                else
                                {
                                    gameData._mainPlayer._playerDeckCard.SpellCard.Add(card);
                                }
                            }
                            break;
                        case 3:
                            {
                                Card card1 = gameData._mainPlayer._playerDeckCard.TrapCard.FirstOrDefault(x => x._CardId == card._CardId);
                                if (card1 != null)
                                {
                                    card1._quantity = card._quantity;
                                }
                                else
                                {
                                    gameData._mainPlayer._playerDeckCard.TrapCard.Add(card);
                                }
                            }
                            break;
                    }
                    gameData._mainPlayer._playerDeckCard.SortAll();
                }
                UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                if (uIDeck != null)
                {
                    uIDeck.UpdateDeckCard(card);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        void HandleRoom(Message message)
        {
            try
            {
                int type = message.readByte();
                switch(type)
                {
                    case 1:
                        {
                            int roomID = message.readInt();
                            UIRoom uIRoom = UIController.Instance.Get<UIRoom>(WindowType.UI_Room);
                            if (uIRoom != null)
                            {
                                uIRoom.isHost = true;
                                uIRoom.SetRoomInfo(roomID.ToString());
                                uIRoom.SetMeInfo(gameData._mainPlayer._namePlayer, gameData._mainPlayer._level);
                                uIRoom.OpenMe();
                                GameData.Instance.CurrentRoom.RoomID = roomID;
                                GameData.Instance.CurrentRoom.HostPlayer=new PlayerState
                                {
                                    PlayerID = gameData._mainPlayer._playerid,
                                    PlayerName = gameData._mainPlayer._namePlayer,
                                    hp = 0
                                };
                            }
                            break;
                        }
                    case 2:
                        {
                            int playerID = message.readInt();
                            string name = message.readUTF();
                            int level = message.readInt();
                            GameData.Instance.CurrentRoom.GuestPlayer = new PlayerState
                            {
                                PlayerID = playerID,
                                PlayerName = name,
                                hp = 0
                            };
                            UIRoom uIRoom = UIController.Instance.Get<UIRoom>(WindowType.UI_Room);
                            if (uIRoom != null)
                            {
                                uIRoom.SetOtherInfo(name, level);
                            }
                            break;
                        }
                    case 3:
                        {
                            UIRoom uIRoom = UIController.Instance.Get<UIRoom>(WindowType.UI_Room);
                            if (uIRoom != null)
                            {
                                uIRoom.CloseMe();
                            }
                            break;
                        }
                    case 4:
                        {
                            int zoomid= message.readInt();
                            int playerID = message.readInt();
                            string name = message.readUTF();
                            int level = message.readInt();
                            
                            UIRoom uIRoom = UIController.Instance.Get<UIRoom>(WindowType.UI_Room);
                            if (uIRoom != null)
                            {
                                uIRoom.SetRoomInfo(zoomid.ToString());
                                uIRoom.SetMeInfo(GameData.Instance._mainPlayer._namePlayer, GameData.Instance._mainPlayer._level);
                                uIRoom.isHost = false;
                                uIRoom.SetOtherInfo(name, level);
                                uIRoom.OpenMe();
                            }
                                break;
                        }
                    case 5:
                        {
                           bool ishost = message.readBool();
                            int hphost = message.readInt();
                            int hpother = message.readInt();
                            GameData.Instance.CurrentRoom.HostPlayer.hp = hphost;
                            GameData.Instance.CurrentRoom.GuestPlayer.hp = hpother;
                            UIMainField uIMainField = UIController.Instance.Get<UIMainField>(WindowType.UI_MainField);
                            if (uIMainField != null)
                            {
                                uIMainField.OpenMe();
                            }
                            break;
                        }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling room message: " + ex.Message);
            }
        }
        void HandleGetEffCard(Message message)
        {
            try
            {
                Card card = new Card
                {
                    _CardId = message.readInt(),
                    _Name = message.readUTF(),
                    _CardType = message.readInt(),
                    _Rarity = message.readUTF(),
                    _Race = message.readInt(),
                    _Hp = message.readInt(),
                    _Attack = message.readInt(),
                    _Level = message.readInt(),
                    _KeyWord = message.readInt(),
                    _Element = message.readInt(),
                };
                byte effectCount = message.readByte();
                for (int i = 0; i < effectCount; i++)
                {
                    int effectId = message.readInt();
                    string effectName = message.readUTF();
                    string description = message.readUTF();
                    string effectType = message.readUTF();
                    bool isPassive = message.readBool();
                    string activezone = message.readUTF();
                    string riggermode = message.readUTF();
                    card.CardEffects.Add(new CardEffects
                    {
                        _id = effectId,
                        _Skillname = effectName,
                        _Des = description,
                        _OnePerTurn = isPassive,
                        _ActiveZone = activezone,
                        _triggerMode = riggermode,
                        _TriggerType = effectType
                    });
                }
                UIInfoCard uIInfoCard = UIController.Instance.Get<UIInfoCard>(WindowType.UI_InfoCard);
                if (uIInfoCard != null)
                {
                    uIInfoCard.ShowItem(card);
                    uIInfoCard.OpenMe();

                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling get effect card message: " + ex.Message);
                return;
            }
        }
        void HandlePlayerDeckCard(Message message)
        {
            try
            {
                PlayerDeckCard deckCard = new PlayerDeckCard();
                deckCard.DeckCardId = message.readInt();
                int count = message.readInt();
                for (int i = 0; i < count; i++)
                {
                    Card card = new Card();
                    card._CardId = message.readInt();
                    card._quantity = message.readInt();
                    card._CardType=message.readByte();
                    card._Name = GameData.Instance.GetName(card._CardId);
                    card._Rarity=GameData.Instance.GetRaity(card._CardId);
                    switch (card._CardType)
                    {
                        case 1:
                            card._Level = GameData.Instance.level(card._CardId);
                            deckCard.MonsterCard.Add(card);
                            break;
                        case 2:
                            deckCard.SpellCard.Add(card);
                            break;
                        case 3:
                            deckCard.TrapCard.Add(card);
                            break;
                    }
                   
                }
                gameData._mainPlayer._playerDeckCard = deckCard;
                gameData._mainPlayer._playerDeckCard.SortAll();
                if (GameController.HasInstance)
                {
                    UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                    if (uIDeck != null)
                    {
                        uIDeck.CreateDeck();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling player deck card message: " + ex.Message);
                return;
            }
        }
        void HandlePlayerDeck(Message message)
        {
            try
            {
                int count = message.readInt();
                for (int i = 0; i < count; i++)
                {
                    PlayerDeck deck = new PlayerDeck();
                    deck._deckID = message.readInt();
                    deck._deckName = message.readUTF();
                    deck.formatType = message.readUTF();
                    deck._isActive = message.readBool();
                    int cardCount = message.readInt();
                    for (int j = 0; j < cardCount; j++)
                    {
                        int cardId = message.readInt();
                        int quantity = message.readInt();
                        deck._card.Add(cardId, quantity);
                    }

                    gameData._mainPlayer._playerDecks.Add(deck);
                }

                if (GameController.HasInstance)
                {
                    UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                    if (uIDeck != null)
                    {
                        uIDeck.CreateDeck();
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling player card message: " + ex.Message);
                return;
            }
        }
        void HandlePlayerCard(Message message)
        {
            try
            {
                int count = message.readInt();
                for (int i = 0; i < count; i++)
                {
                    Card card = new Card();
                    card._CardId = message.readInt();
                    card._quantity = message.readInt();
                    card._Name = GameData.Instance.GetName(card._CardId);
                    card._Rarity=GameData.Instance.GetRaity(card._CardId);
                    int type = message.readByte();
                    if(type is 1)
                    {
                        card._Level = GameData.Instance.level(card._CardId);
                        gameData._mainPlayer._playerCardData.MonsterCard.Add(card);
                    }else
                    if(type is 2)
                    {
                        gameData._mainPlayer._playerCardData.SpellCard.Add(card);
                    }
                    else
                    {
                        gameData._mainPlayer._playerCardData.TrapCard.Add(card);
                    }

                }
                gameData._mainPlayer._playerCardData.SortAll();
                if (GameController.HasInstance)
                {
                    UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                    if (uIDeck != null)
                    {
                        uIDeck.CreatePlayerCard();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling player card message: " + ex.Message);
                return;
            }
        }
        void HandleInfo(Message message)
        {
            try
            {
                gameData._mainPlayer._playerid = message.readInt();
                gameData._mainPlayer._namePlayer = message.readUTF();
                gameData._mainPlayer._level = message.readInt();
                gameData._mainPlayer._gold = message.readInt();
                gameData._mainPlayer._diamond = message.readInt();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling info message: " + ex.Message);
                return;
            }


        }
        void HandleCreatePlayer(Message message)
        {
            bool t = message.readBool();
            if (t)
            {
                LoginController.Instance.ActiveCreatePlayer();
            }
        }
        void HandleLoginSucces(Message message)
        {
            bool t = message.readBool();
            if (t)
            {
                SceneManager.LoadScene(1);
            }
        }
        public void HandleGetAllCard(Message msg)
        {
            try
            {
                int totalCards = msg.readInt();
                for (int i = 0; i < totalCards; i++)
                {
                    Card card1 = new Card();
                    card1._CardId = msg.readInt();
                    card1._Name = msg.readUTF();
                    card1._Rarity = msg.readUTF();
                    card1._KeyWord = msg.readByte();
                    card1._CardType = msg.readByte();
                    if(card1._CardType is 1)
                    {
                        card1._Hp = msg.readShort();
                        card1._Attack = msg.readShort();
                        card1._Element = msg.readByte();
                        card1._Level = msg.readByte();
                        card1._Race = msg.readByte();
                    }
                    

                    GameData.Instance._allCard.Add(card1);
                }


            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling get all card message: " + ex.Message);
            }
        }

    }
}

