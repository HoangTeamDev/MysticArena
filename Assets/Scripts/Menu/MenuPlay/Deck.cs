using System.Collections.Generic;
using UnityEngine;
using Menu.Card;
namespace Menu.MenuPlay
{
    public class Deck
    {
        /* private List<Card> cards;
         private const int MaxCards = 30; // Tối đa 30 lá bài
         private const int MinCards = 20; // Tối thiểu 20 lá bài
         private const int MaxCopiesPerCard = 2; // Tối đa 2 bản sao của một lá bài

         public Deck(List<Card> initialCards)
         {
             cards = new List<Card>();

             foreach (var card in initialCards)
             {
                 AddCard(card);
             }

             Shuffle(); // Xáo bài khi khởi tạo
         }

         // ✅ Xáo bài
         public void Shuffle()
         {
             for (int i = 0; i < cards.Count; i++)
             {
                 int randomIndex = Random.Range(0, cards.Count);
                 Card temp = cards[i];
                 cards[i] = cards[randomIndex];
                 cards[randomIndex] = temp;
             }
         }

         // ✅ Rút bài từ bộ bài
         public Card DrawCard()
         {
             if (cards.Count == 0)
             {
                 Debug.Log("Không còn bài để rút!");
                 return null;
             }

             Card topCard = cards[0];
             cards.RemoveAt(0);
             return topCard;
         }

         // ✅ Thêm bài vào bộ bài với giới hạn số lượng
         public bool AddCard(Card newCard)
         {
             if (cards.Count >= MaxCards)
             {
                 Debug.Log($"Không thể thêm {newCard.Name}, bộ bài đã đầy ({MaxCards} lá)!");
                 return false;
             }

             int count = cards.FindAll(c => c.ID == newCard.ID).Count;
             if (count >= MaxCopiesPerCard)
             {
                 Debug.Log($"Không thể thêm {newCard.Name}, đã đạt giới hạn {MaxCopiesPerCard} bản sao!");
                 return false;
             }

             cards.Add(newCard);
             return true;
         }

         // ✅ Xóa bài khỏi bộ bài
         public void RemoveCard(Card card)
         {
             if (cards.Contains(card))
             {
                 cards.Remove(card);
                 Debug.Log($"{card.Name} đã bị xóa khỏi bộ bài.");
             }
             else
             {
                 Debug.Log($"{card.Name} không có trong bộ bài.");
             }
         }

         // ✅ Kiểm tra số lượng bài trong bộ bài
         public bool IsDeckValid()
         {
             return cards.Count >= MinCards && cards.Count <= MaxCards;
         }

         // ✅ Lấy toàn bộ danh sách bài
         public List<Card> GetCards()
         {
             return cards;
         }

         // ✅ Lấy bài ngẫu nhiên từ bộ bài
         public Card DrawRandomCard()
         {
             if (cards.Count == 0)
             {
                 Debug.Log("Không còn bài trong bộ bài!");
                 return null;
             }

             int randomIndex = Random.Range(0, cards.Count);
             Card drawnCard = cards[randomIndex];
             cards.RemoveAt(randomIndex);
             return drawnCard;
         }

         // ✅ Kiểm tra số lượng bản sao của một lá bài trong bộ bài
         public int GetCardCount(int cardID)
         {
             return cards.FindAll(c => c.ID == cardID).Count;
         }
     }*/
    }
}

