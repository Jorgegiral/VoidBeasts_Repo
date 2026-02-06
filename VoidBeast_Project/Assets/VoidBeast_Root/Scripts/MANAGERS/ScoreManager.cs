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
    [SerializeField] AudioClip clickSound;


    public void SubmitScore()
    {
        Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);

        Leaderboard.instance.SetLeaderboardEntry(inputName.text, int.Parse(inputNights.text));
    }
    public void BackToMenu()
    {
        Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void NextButton()
    {
        Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);

        leaderboardImage.SetActive(true);
        youDiedImage.SetActive(false);
        nightSurvived.text = DayNightSystem.Instance.nightNumber.ToString();
        enemiesKilled.text = PlayerStats.instance.enemykilledCount.ToString();
        MoneyEarned.text = MoneySystem.instance.totalMoneyEarned.ToString();
        
    }
}
