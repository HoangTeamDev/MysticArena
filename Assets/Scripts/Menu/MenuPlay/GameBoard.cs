
using Menu.System;
using System.Collections.Generic;
using UnityEngine;
namespace Menu.MenuPlay
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard Instance;

      

        public Deck PlayerDeck;
        public Deck OpponentDeck;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
          
        }

        /*public MonsterCard DrawMonsterFromDeck(Player player, int level, bool allowHigherLevel = false)
        {
            Deck deck = player == GameManager.Instance.Player ? PlayerDeck : OpponentDeck;
            MonsterCard bestMatch = null;

            foreach (var card in deck.GetCards())
            {
                if (card is MonsterCard monster)
                {
                    if (monster.Level == level)
                    {
                        deck.RemoveCard(card);
                        return monster;
                    }
                    if (allowHigherLevel && monster.Level > level)
                    {
                        if (bestMatch == null || monster.Level < bestMatch.Level)
                        {
                            bestMatch = monster;
                        }
                    }
                }
            }

            if (bestMatch != null)
            {
                deck.RemoveCard(bestMatch);
                return bestMatch;
            }

            return null; // Không tìm thấy quái phù hợp
        }*/

        
        public void EndTurn()
        {
            Debug.Log("Lượt kết thúc!");
        }

        public void CheckWinCondition()
        {
            if (GameManager.Instance.Player.HP <= 0)
            {
                Debug.Log("Người chơi thua trận!");
            }
            else if (GameManager.Instance.Opponent.HP <= 0)
            {
                Debug.Log("Người chơi thắng!");
            }
        }
    }
}

