using CardData;
using Menu.System;
using Player;
using System;
using System.Collections.Generic;
using UI.SystemUI;
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
                        int cardID = message.readInt();
                        int quantity = message.readInt();
                        if (quantity == 0)
                        {
                            gameData._mainPlayer._playerDeckCard.Cards.Remove(cardID);
                        }
                        else
                        {
                            gameData._mainPlayer._playerDeckCard.Cards[cardID] = quantity;
                        }
                        UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                        if (uIDeck != null)
                        {
                            uIDeck.UpdateDeckCard(cardID, quantity);
                        }
                    }
                    break;
                case 9:
                    {
                        int cardID = message.readInt();
                        int quantity = message.readInt();
                        if (quantity == 0)
                        {
                            gameData._mainPlayer._playerCardData.AllCard.Remove(cardID);
                        }
                        else
                        {
                            gameData._mainPlayer._playerCardData.AllCard[cardID] = quantity;
                        }
                        UIDeck uIDeck = UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
                        if (uIDeck != null)
                        {
                            uIDeck.UpdatePlayerCard(cardID,quantity);
                        }
                    }
                    break;

                default:
                    Debug.LogWarning("Unknown opcode: " + message);
                    break;
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
                    int cardId = message.readInt();
                    int quantity = message.readInt();
                    deckCard.Cards.Add(cardId, quantity);
                }
                gameData._mainPlayer._playerDeckCard= deckCard;

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
               
                if (GameController.HasInstance) {
                    UIDeck uIDeck=UIController.Instance.Get<UIDeck>(WindowType.UI_deck);
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

                    int cardId = message.readInt();
                    int quantity = message.readInt();
                    if (gameData._mainPlayer._playerCardData.AllCard.ContainsKey(cardId))
                    {
                        gameData._mainPlayer._playerCardData.AllCard[cardId] = quantity;
                    }
                    else
                    {
                        gameData._mainPlayer._playerCardData.AllCard.Add(cardId, quantity);
                    } 
                      
                }

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
            catch(Exception ex)
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
                int totalBatches = msg.readInt();
                int batchIndex = msg.readInt();
                int countInBatch = msg.readInt();

                Debug.Log($"Receive batch {batchIndex + 1}/{totalBatches}, cards in batch = {countInBatch}, totalCards = {totalCards}");

                for (int i = 0; i < countInBatch; i++)
                {
                    int key = msg.readInt();
                    string name = msg.readUTF();
                    int attack = msg.readInt();
                    int hp = msg.readInt();
                    int cardType = msg.readInt();
                    int level = msg.readInt();
                    string rarity = msg.readUTF();
                    int race = msg.readInt();
                    int element = msg.readInt();
                    int keyword = msg.readInt();

                    byte effectCount = msg.readByte();

                    List<CardEffects> effects = new List<CardEffects>();
                    for (int j = 0; j < effectCount; j++)
                    {
                        int id = msg.readInt();
                        string skillName = msg.readUTF();
                        string des = msg.readUTF();
                        string triggerType = msg.readUTF();
                        bool onePerTurn = msg.readBool();
                        string activeZone = msg.readUTF();
                        string triggerMode = msg.readUTF();

                        effects.Add(new CardEffects
                        {
                            _id = id,
                            _Skillname = skillName,
                            _Des = des,
                            _TriggerType = triggerType,
                            _OnePerTurn = onePerTurn,
                            _ActiveZone = activeZone,
                            _triggerMode = triggerMode
                        });
                    }

                    Card card = new Card
                    {
                        _CardId = key,
                        _Name = name,
                        _Attack = attack,
                        _Hp = hp,
                        _CardType = cardType,
                        _Level = level,
                        _Rarity = rarity,
                        _Race = race,
                        _Element = element,
                        _KeyWord = keyword,
                        CardEffects = effects
                    };

                    // Thêm vào dictionary / list của client
                    GameData.Instance._allCard.Add(card);
                }

                if (batchIndex == totalBatches - 1)
                {
                    Debug.Log("Finished receiving all card batches.");
                    // Có thể trigger UI refresh ở đây
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error handling get all card message: " + ex.Message);
            }
        }

    }
}

