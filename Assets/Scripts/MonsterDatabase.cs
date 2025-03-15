using System.Collections.Generic;
using UnityEngine;

public class MonsterDatabase : MonoBehaviour
{
    public static MonsterDatabase Instance;

    [Header("Danh sách quái vật")]
    public List<MonsterData> AllMonsters;
    public List<SpellData> Spells;
    public List<MonsterCard> Monsters;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ✅ Lấy quái vật theo ID
    public MonsterData GetMonsterByID(int id)
    {
        return AllMonsters.Find(monster => monster.ID == id);
    }

    // ✅ Lấy quái vật theo tên
    public MonsterData GetMonsterByName(string name)
    {
        return AllMonsters.Find(monster => monster.Name == name);
    }
    private void Start()
    {
        // Kiểm tra danh sách MonsterData có dữ liệu không
        if (AllMonsters == null || AllMonsters.Count == 0)
        {
            Debug.LogError("Không có quái vật nào trong danh sách AllMonsters!");
            return;
        }
        for (int i = 0; i < AllMonsters.Count; i++)
        {
            Monsters[i].Onload(
          AllMonsters[i].ID,
          AllMonsters[i].Name,
          AllMonsters[i].Level,
          AllMonsters[i].ATK,
          AllMonsters[i].HP,
          AllMonsters[i].Element,
          AllMonsters[i].Race,
          AllMonsters[i].Abilities,
          AllMonsters[i].idEvolutionTarget);
            Debug.Log($"Đã tạo quái vật: {Monsters[i].Name}");

        }


    }
}
