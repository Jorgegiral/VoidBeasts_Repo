using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
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
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextButton()
    {
        leaderboardImage.SetActive(true);
        youDiedImage.SetActive(false);
        nightSurvived.text = DayNightSystem.Instance.nightNumber.ToString();
        
    }
}
