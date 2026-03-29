using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UIScripts.SystemUI;
using System;
using CardData;
using Menu.System;
using Player;
using UI.UIWindow;
using UI.SystemUI;
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
        void HandleGetAllCard(Message message)
        {
            try
            {
                int cout = message.readInt();
                for (int i = 0; i < cout; i++)
                {
                    Card card = new Card();
                    card._CardId = message.readInt();
                    card._Name = message.readUTF();
                    card._Attack = message.readInt();
                    card._Hp = message.readInt();
                    card._CardType = message.readInt();
                    card._Level = message.readInt();
                    card._Rarity = message.readUTF();
                    card._Race = message.readInt();
                    card._Element = message.readInt();
                    card._KeyWord = message.readInt();
                    int count = message.readByte();
                    for (int j = 0; j < count; j++)
                    {
                        CardEffects effect = new CardEffects();
                        effect._id = message.readInt();
                        effect._Skillname = message.readUTF();
                        effect._Des = message.readUTF();
                        effect._TriggerType = message.readUTF();
                        effect._OnePerTurn = message.readBool();
                        effect._ActiveZone = message.readUTF();
                        effect._triggerMode = message.readUTF();
                        card.CardEffects.Add(effect);
                    }
                    GameData.Instance._allCard.Add(card);
                }
            }
            catch(Exception ex)
            {
                Debug.LogError("Error handling get all card message: " + ex.Message);
                return;
            }
           
        }
       
    }
}

