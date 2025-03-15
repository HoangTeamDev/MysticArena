using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player Player;
    public Player Opponent;
    public GameBoard Board;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        Debug.Log("Trò chơi bắt đầu!");

        Board = GameBoard.Instance;

        // Khởi tạo bộ bài cho mỗi người chơi
        Player = new Player(new Deck(GenerateStarterDeck()));
        Opponent = new Player(new Deck(GenerateStarterDeck()));

        // Xáo bài
        Player.Deck.Shuffle();
        Opponent.Deck.Shuffle();

        // Rút 5 lá bài khởi đầu
        for (int i = 0; i < 5; i++)
        {
            Player.DrawCard();
            Opponent.DrawCard();
        }

        Debug.Log("Bắt đầu game, mỗi người chơi có 5 lá bài!");
    }

    private List<Card> GenerateStarterDeck()
    {
        List<Card> deck = new List<Card>();

        for (int i = 0; i < 20; i++)
        {
            deck.Add(new MonsterCard(i, $"Quái Vật {i}", "Mô tả...", Random.Range(1, 6), 1000, 1000, ElementType.Fire, RaceType.Dragon, new List<Ability>()));
        }

        return deck;
    }

    public void NextTurn()
    {
        Debug.Log("Chuyển sang lượt mới!");
        Player.DrawCard();
    }

    public void CheckWinCondition()
    {
        if (Player.HP <= 0)
        {
            Debug.Log("Người chơi thua!");
        }
        else if (Opponent.HP <= 0)
        {
            Debug.Log("Người chơi thắng!");
        }
    }

    public void HandleSummoning(Player player, MonsterCard monster, List<MonsterCard> sacrifices = null)
    {
        if (monster.Level <= 4)
        {
            SummonManager.TryNormalSummon(player, monster);
        }
        else if (monster.Level == 5 || monster.Level == 6)
        {
            SummonManager.TryTributeSummon(player, sacrifices, monster);
        }
        else if (monster.CanEvolve)
        {
            SummonManager.TryEvolutionSummon(player, monster);
        }
    }
}
