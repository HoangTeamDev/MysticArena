using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        Debug.Log("Trò chơi bắt đầu!");
        // Khởi tạo gameboard, shuffle deck, chia bài
    }

    public void NextTurn()
    {
        Debug.Log("Chuyển sang lượt mới!");
    }

    public void CheckWinCondition()
    {
        Debug.Log("Kiểm tra điều kiện thắng/thua!");
    }
}

