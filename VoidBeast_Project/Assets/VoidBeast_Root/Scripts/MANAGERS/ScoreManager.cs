using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputNights;
    [SerializeField] TMP_InputField inputName;
    [SerializeField] GameObject leaderboardImage;
    [SerializeField] GameObject youDiedImage;
    [SerializeField] TMP_Text nightSurvived;
    [SerializeField] TMP_Text enemiesKilled;
    [SerializeField] TMP_Text MoneyEarned;


    public void SubmitScore()
    {
        Leaderboard.instance.SetLeaderboardEntry(inputName.text, int.Parse(inputNights.text));
    }
    
}
