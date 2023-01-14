using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static List<Player> players;

    public static string currentPlayer;
    public static int playerBestScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        players = new List<Player>();
    }

    public static void StartNew()
    {
        if (currentPlayer != null) SceneManager.LoadScene(1);

        playerBestScore = 20;
    }

    public static void SavePlayer(string name)
    {
        currentPlayer = name;

        if (GetPlayer(currentPlayer) == null)
        {
            players.Add(new Player(name, 0));
        }
    }

    public static void SetPlayerScore(int score)
    {
        GetPlayer(currentPlayer).score = score;
    }

    public static Player GetPlayer(string name)
    {
        return players.Find(Player => Player.name == name);
    }

    public class Player
    {
        public string name;
        public int score;

        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
