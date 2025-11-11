using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyPowerUP : MonoBehaviour
{
    [SerializeField] DailyPowerUpsSO[] CommonPowerUp;
    [SerializeField] DailyPowerUpsSO[] RarePowerUp;
    [SerializeField] DailyPowerUpsSO[] legendaryPowerUp;
    [SerializeField] Image[] ImagePowerUp;
    [SerializeField] TMP_Text[] rarityText;
    [SerializeField] TMP_Text[] upgradeText;
    [SerializeField] TMP_Text[] infoText;
    [SerializeField] TMP_Text actualMoney;
    private DailyPowerUpsSO[] selectedPowerUps;



    
    public void StartPowerUp()
    {
        gameObject.SetActive(true);
        
    }
    public void ClosePowerUp()
    {
        gameObject.SetActive(false);
    }
}
