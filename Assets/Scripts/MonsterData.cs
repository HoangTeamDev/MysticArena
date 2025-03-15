using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Card Game/Monster Data")]
public class MonsterData : ScriptableObject
{
    public int ID;
    public string Name;   
    public RaceType Race; // Tộc
    public ElementType Element; // Hệ

    public int ATK;
    public int HP;
    public int Level;
    public List<string> Keywords; // Từ khóa

    public int idEvolutionTarget; // Tiến hóa thành quái nào?
    public List<Ability> Abilities; // Kỹ năng của quái

    
}
