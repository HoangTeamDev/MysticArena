using UnityEngine;

/// <summary>
/// Ô chứa bài trên sân đấu
/// </summary>
public class CardSlot
{
    public Card Card { get; private set; } // Lá bài hiện tại trong ô
    public bool IsOccupied => Card != null; // Kiểm tra xem ô có bài không
    public int Position { get; private set; } // Vị trí trên sân (0-5)

    public CardSlot(int position)
    {
        Position = position;
        Card = null;
    }

    /// <summary>
    /// Đặt lá bài vào ô này
    /// </summary>
    public bool PlaceCard(Card newCard)
    {
        if (IsOccupied) return false; // Không thể đặt nếu đã có bài
        Card = newCard;
        Debug.Log($"Đặt {newCard.Name} vào vị trí {Position}.");
        return true;
    }

    /// <summary>
    /// Gỡ lá bài khỏi ô
    /// </summary>
    public void RemoveCard()
    {
        if (IsOccupied)
        {
            Debug.Log($"{Card.Name} rời khỏi vị trí {Position}.");
            Card = null;
        }
    }
    public void DestroyCard()
    {

    }
    /// <summary>
    /// Kiểm tra lá bài có thể tấn công không
    /// </summary>
    public bool CanAttack()
    {
        return IsOccupied && Card is MonsterCard monster && monster.CanAttack;
    }
}
