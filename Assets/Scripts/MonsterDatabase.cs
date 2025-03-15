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
        Monsters[0].Onload(AllMonsters[0].ID, AllMonsters[0].Name, AllMonsters[0].Level, AllMonsters[0].ATK, AllMonsters[0].HP);
        Debug.Log($"Đã tạo quái vật: {Monsters[0].Name}");
    }
}
