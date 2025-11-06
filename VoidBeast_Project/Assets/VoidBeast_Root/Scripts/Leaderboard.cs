using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using Dan.Main;
public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] List<TextMeshProUGUI> playerNames;
    [SerializeField] List<TextMeshProUGUI> playerNights;
    private string publicLeaderboardKey = "3f80811e03415ba201e306ad00241bf65a306df662ae7a6f2863fed845355dfc";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GetLeaderboard();
    }
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLenght = (msg.Length < playerNames.Count) ? msg.Length : playerNames.Count;
            for (int i = 0; i < loopLenght; i++)
            {
                playerNames[i].text = msg[i].Username;
                playerNights[i].text = msg[i].Score.ToString();
            }
        }));
    }
    public void SetLeaderboardEntry(string username, int nights)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey,username, nights, ((msg) =>
        {
            username.Substring(0, 16);
            //if(System.Array.IndexOf(badWords, name) != -1) return;
            GetLeaderboard();
        }));
    }
}
