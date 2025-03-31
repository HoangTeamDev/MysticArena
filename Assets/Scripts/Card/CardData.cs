using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/CardData")]
public class CardData : ScriptableObject
{
    public bool IsSpell;//xem là spell hay không
    public int ID;//id
    public string Name; //Name  
    public RaceType Race; // Tộc
    public ElementType Element; // Hệ
    public KeyWords Keywords; // Từ khóa
    public Quality quality;
    public int limit;
    public int ATK;//atk
    public int HP;//hp
    public Level Level;//levle
    public SpellType Type;//type Spell
    public List<Ability> Abilities; // Kỹ năng của quái

    
}
