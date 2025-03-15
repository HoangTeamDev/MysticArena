using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Card Game/Monster Data")]
public class MonsterData : ScriptableObject
{
    public int ID;//id
    public string Name; //Name  
    public RaceType Race; // Tộc
    public ElementType Element; // Hệ
    public KeyWords Keywords; // Từ khóa
    public int ATK;//atk
    public int HP;//hp
    public int Level;//levle
    public int idEvolutionTarget; // Tiến hóa thành quái nào?
    public List<Ability> Abilities; // Kỹ năng của quái

    
}
