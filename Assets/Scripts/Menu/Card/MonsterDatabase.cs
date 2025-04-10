using System.Collections.Generic;
using UnityEngine;
namespace Menu.Card
{
    public class MonsterDatabase : MonoBehaviour
    {
        public static MonsterDatabase Instance;

        [Header("Danh sách quái vật")]
        public List<CardData> AllCards;

        public List<MonsterCard> Monsters;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        // ✅ Lấy quái vật theo ID
        public CardData GetMonsterByID(int id)
        {
            return AllCards.Find(monster => monster.ID == id);
        }

        // ✅ Lấy quái vật theo tên
        public CardData GetMonsterByName(string name)
        {
            return AllCards.Find(monster => monster.Name == name);
        }

        private void Start()
        {
            // Kiểm tra danh sách MonsterData có dữ liệu không
            if (AllCards == null || AllCards.Count == 0)
            {
                Debug.LogError("Không có quái vật nào trong danh sách AllMonsters!");
                return;
            }
            for (int i = 0; i < AllCards.Count; i++)
            {
                Monsters[i].Onload(
              AllCards[i].ID,
              AllCards[i].Name,
              (int)AllCards[i].Level,
              AllCards[i].ATK,
              AllCards[i].HP,
              AllCards[i].Element,
              AllCards[i].Race,
              AllCards[i].Abilities
             );
                Debug.Log($"Đã tạo quái vật: {Monsters[i].Name}");

            }


        }
    }
}

