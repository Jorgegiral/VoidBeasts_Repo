using TMPro;
using Unity.VisualScripting;
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
    private void PickPowerUps()
    {
        selectedPowerUps = new DailyPowerUpsSO[3];
        for (int i = 0; i < selectedPowerUps.Length; i++)
        {
            ImagePowerUp[i].sprite = selectedPowerUps[i].raritySprite;
            rarityText[i].text = selectedPowerUps[i].rarityName.ToString();
            upgradeText[i].text = selectedPowerUps[i].type.ToString();
            infoText[i].text = selectedPowerUps[i].description;
        }
    }
}
}
