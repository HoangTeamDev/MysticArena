using Menu.Card;
using Menu.System;
using System.Collections.Generic;
using UnityEngine;
namespace Menu.MenuPlay
{
    public class GameBoard : MonoBehaviour
    {
        public static GameBoard Instance;

        public List<CardSlot> PlayerField;  // Ô bài của người chơi
        public List<CardSlot> OpponentField; // Ô bài của đối thủ

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
            PlayerField = new List<CardSlot>();
            OpponentField = new List<CardSlot>();

            for (int i = 0; i < 6; i++) // Giả sử mỗi người chơi có 6 ô bài
            {
                PlayerField.Add(new CardSlot(i));
                OpponentField.Add(new CardSlot(i));
            }

            Debug.Log("Game Board đã khởi tạo!");
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

        public void SummonMonster(Player player, MonsterCard monster, int slotIndex)
        {
            List<CardSlot> field = (player == GameManager.Instance.Player) ? PlayerField : OpponentField;

            if (slotIndex < 0 || slotIndex >= field.Count || field[slotIndex].IsOccupied)
            {
                Debug.Log("Không thể triệu hồi quái vật vào vị trí này!");
                return;
            }

            field[slotIndex].PlaceCard(monster);
            Debug.Log($"{monster.Name} đã được triệu hồi vào vị trí {slotIndex}");
        }

        public void Attack(MonsterCard attacker, MonsterCard defender)
        {
            if (attacker == null || defender == null) return;

            float damageMultiplier = ElementInteraction.GetDamageMultiplier(attacker.Element, defender.Element);
            int finalDamage = Mathf.RoundToInt(attacker.ATK * damageMultiplier);

            //defender.TakeDamage(finalDamage);
            Debug.Log($"{attacker.Name} tấn công {defender.Name} gây {finalDamage} sát thương!");

            if (defender.HP <= 0)
            {
                //defender.DestroyCard();
            }
        }

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

