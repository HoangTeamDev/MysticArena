using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Card Game/Spell Data")]
public class SpellData : ScriptableObject
{
    public int ID;
    public string Name;
    public SpellType Type; // Trang bị hoặc kích hoạt
    public KeyWords Keywords; // Các từ khóa liên quan
    public List<Ability> Abilities;
    public Func<Player, GameBoard, bool> Condition; // Điều kiện kích hoạt
    public Action<Player, GameBoard> Effect; // Hiệu ứng của bài phép

    public void ActivateEffect(Player player, GameBoard gameBoard)
    {
        if (Condition == null || Condition(player, gameBoard))
        {
            Debug.Log($"Kích hoạt bài phép: {Name}");
            Effect?.Invoke(player, gameBoard);
        }
        else
        {
            Debug.Log($"Không thể kích hoạt {Name}, điều kiện chưa đạt!");
        }
    }
}
