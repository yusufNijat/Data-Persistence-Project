using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        LoadPlayers();
    }

    public static void StartNew()
    {
        if (currentPlayer != null) SceneManager.LoadScene(1);

        playerBestScore = GetPlayer(currentPlayer).score;
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
        playerBestScore = score;
    }

    public static Player GetPlayer(string name)
    {
        return players.Find(Player => Player.name == name);
    }

    public static void UpdatePlayers()
    {
        PlayerList pl = new PlayerList(players);
        string json = JsonUtility.ToJson(pl);
        string saveFile = Application.persistentDataPath + "/gamedata.json";

        File.WriteAllText(saveFile, json);
    }

    public static void LoadPlayers()
    {
        string saveFile = Application.persistentDataPath + "/gamedata.json";

        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            PlayerList pl = JsonUtility.FromJson<PlayerList>(json);
            players = pl.list;
        }
        else
        {
            players = new List<Player>();
        }
        Debug.Log(players.ToString());
    }

    [System.Serializable]
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

    [System.Serializable]
    public class PlayerList
    {
        public List<Player> list;

        public PlayerList(List<Player> list)
        {
            this.list = list;
        }
    }
}
