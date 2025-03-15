using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Card Game/Monster Data")]
public class MonsterData : ScriptableObject
{
    public int ID;
    public string Name;
    [TextArea] public string Description;

    public RaceType Race; // Tộc
    public ElementType Element; // Hệ

    public int ATK;
    public int HP;
    public int Level;
    public List<string> Keywords; // Từ khóa

    public MonsterData EvolutionTarget; // Tiến hóa thành quái nào?
    public List<Ability> Abilities; // Kỹ năng của quái

    public void PrintData()
    {
        Debug.Log($"Quái: {Name}, ATK: {ATK}, HP: {HP}, Level: {Level}, Tiến hóa: {EvolutionTarget?.Name}");
    }
}
