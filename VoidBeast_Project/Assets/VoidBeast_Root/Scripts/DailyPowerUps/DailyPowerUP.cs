using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DailyPowerUP : MonoBehaviour
{
    [SerializeField] DailyPowerUpsSO[] allPowerUps;
    [SerializeField] Image[] ImagePowerUp;
    [SerializeField] TMP_Text[] rarityText;
    [SerializeField] TMP_Text[] upgradeText;
    [SerializeField] TMP_Text[] infoText;
    [SerializeField] TMP_Text[] costMoneyText;
    public DailyPowerUpsSO[] selectedPowerUps;
    [SerializeField] AudioClip startPickSound;
    [SerializeField] AudioClip clickSound;
    string upgradeName;
    private int selectedIndex = -1;
    [SerializeField] GameObject firstSelectedMenu;

    int PowerUpCost = 10;
    
    public void StartPowerUp()
    {
        gameObject.SetActive(true);
        PickPowerUps();
        Settings.instance.PlaySoundFXClip(startPickSound, transform, 1f);

        //for? solo para 3
        costMoneyText[0].text = PowerUpCost.ToString();
        costMoneyText[1].text = PowerUpCost.ToString();
        costMoneyText[2].text = PowerUpCost.ToString();
        EventSystem.current.SetSelectedGameObject(firstSelectedMenu);
        Time.timeScale = 0f;

    }
    public void ClosePowerUp()
    {
        gameObject.SetActive(false);
        Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);
        Time.timeScale = 1f;


    }

    public DailyPowerUpsSO SendClickPowerUpInfo()
    {
        if (selectedIndex < 0)
        {
            gameObject.SetActive(false);
            return null;
        }
        if (CanAfford())
        {
            DailyPowerUpsSO chosen = selectedPowerUps[selectedIndex];
            PlayerStats.instance.powerUpChosen = chosen;
            PlayerStats.instance.ApplyStats();
            Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);
            gameObject.SetActive(false);

            return chosen;
        }
        else
        {
            return null;
        }
    }
    public void SelectPowerUp(int index)
    {
        selectedIndex = index;
        Time.timeScale = 1f;
        SendClickPowerUpInfo();
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
    private bool CanAfford()
    {
        if(MoneySystem.instance.money >= PowerUpCost)
        {
            MoneySystem.instance.BuyMoney(PowerUpCost);
            PowerUpCost += 20;
            return true;
        }
        else
        {
            return false;
        }      
    }

    private DailyPowerUpsSO RandomFrom(List<DailyPowerUpsSO> pool, PowerUpRarity rarity)
    {
        var list = pool.Where(p => p.rarityName == rarity).ToList();
        return list[Random.Range(0, list.Count)];
    }
}


