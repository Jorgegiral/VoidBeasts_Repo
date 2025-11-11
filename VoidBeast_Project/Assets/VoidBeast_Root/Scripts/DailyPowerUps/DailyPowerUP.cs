using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DailyPowerUP : MonoBehaviour
{
    [SerializeField] DailyPowerUpsSO[] allPowerUps;
    [SerializeField] Image[] ImagePowerUp;
    [SerializeField] TMP_Text[] rarityText;
    [SerializeField] TMP_Text[] upgradeText;
    [SerializeField] TMP_Text[] infoText;
    [SerializeField] TMP_Text actualMoney;
    public DailyPowerUpsSO[] selectedPowerUps;
    string upgradeName;
    private int selectedIndex = -1;

    int PowerUpCost;

    
    public void StartPowerUp()
    {
        gameObject.SetActive(true);
        PickPowerUps();
    }
    public void ClosePowerUp()
    {
        gameObject.SetActive(false);
    }
    public DailyPowerUpsSO OnClickPowerUp()
    {

        if (selectedIndex < 0)
        {
            gameObject.SetActive(false);
            return null;
        }

        DailyPowerUpsSO chosen = selectedPowerUps[selectedIndex];
        PlayerStats.instance.powerUpChosen = chosen;

        return chosen;
    }
    public void SelectPowerUp(int index)
    {
        selectedIndex = index;
        OnClickPowerUp();
        gameObject.SetActive(false);
    }
    private void PickPowerUps()
    {
        selectedPowerUps = new DailyPowerUpsSO[3];
        List<DailyPowerUpsSO> powerUpList = new List<DailyPowerUpsSO>(allPowerUps);


        for(int i= 0; i< 3; i++)
        {
            DailyPowerUpsSO picked = GetRandomPowerUps(powerUpList);
            selectedPowerUps[i] = picked;
            powerUpList.Remove(picked);
        }
        for (int i = 0; i < selectedPowerUps.Length; i++)
        {
            ImagePowerUp[i].sprite= selectedPowerUps[i].raritySprite;
            rarityText[i].text = selectedPowerUps[i].rarityName.ToString();
            upgradeText[i].text = selectedPowerUps[i].type.ToString();
            infoText[i].text = selectedPowerUps[i].description;
        }
    }
    private DailyPowerUpsSO GetRandomPowerUps(List<DailyPowerUpsSO> availablePowerUps)
    {
        float commonWeight = 80f;
        float rareWeight = 18f;
        float legendaryWeight = 2f;

        float total = commonWeight + rareWeight + legendaryWeight;
        float roll = Random.Range(0f, total);

        if (roll < commonWeight)
            return RandomFrom(availablePowerUps, PowerUpRarity.Common);

        roll -= commonWeight;

        if (roll < rareWeight)
            return RandomFrom(availablePowerUps, PowerUpRarity.Rare);

        return RandomFrom(availablePowerUps, PowerUpRarity.Legendary);
    }

    private DailyPowerUpsSO RandomFrom(List<DailyPowerUpsSO> pool, PowerUpRarity rarity)
    {
        var list = pool.Where(p => p.rarityName == rarity).ToList();
        return list[Random.Range(0, list.Count)];
    }
}


