using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DailyPowerUP : MonoBehaviour
{
    [Header("PowerUps")]
    [SerializeField] DailyPowerUpsSO[] allPowerUps;
    [Header("References")]
    [SerializeField] Image[] ImagePowerUp;
    [SerializeField] TMP_Text[] rarityText;
    [SerializeField] TMP_Text[] upgradeText;
    [SerializeField] TMP_Text[] infoText;
    [SerializeField] AudioClip startPickSound;
    [SerializeField] AudioClip clickSound;

    public DailyPowerUpsSO[] selectedPowerUps;
    string upgradeName;
    private int selectedIndex = -1;
    [SerializeField] GameObject firstSelectedMenu;
    bool enemies = false;
    bool upgradeselect = false;

    public void StartPowerUp()
    {
        gameObject.SetActive(true);
        PickPowerUps();
        Settings.instance.PlaySoundFXClip(startPickSound, transform, 1f);
        EventSystem.current.SetSelectedGameObject(firstSelectedMenu);
        Time.timeScale = 0f;
        //Jorge:
        if (TutorialManager.instance.step == 10)
        {
            if (!enemies)
            {
                enemies = true;
                TutorialManager.instance.CompleteStep();
            }
        }
    }
    public void ClosePowerUp()
    {
        gameObject.SetActive(false);
        Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);
        Time.timeScale = 1f;

        if (TutorialManager.instance.step == 11)
        {
            if (!upgradeselect)
            {
                upgradeselect = true;
                TutorialManager.instance.CompleteStep();
            }
        }

    }

    public DailyPowerUpsSO SendClickPowerUpInfo()
    {
        if (selectedIndex < 0)
        {
            gameObject.SetActive(false);
            return null;
        }
       
            DailyPowerUpsSO chosen = selectedPowerUps[selectedIndex];
            PlayerStats.instance.powerUpChosen = chosen;
            PlayerStats.instance.ApplyStats();
            Settings.instance.PlaySoundFXClip(clickSound, transform, 1f);
            gameObject.SetActive(false);

            return chosen;
    }
    public void SelectPowerUp(int index)
    {
        selectedIndex = index;
        Time.timeScale = 1f;
        SendClickPowerUpInfo();
        //Jorge:
        if (TutorialManager.instance.step == 11)
        {
            if (!upgradeselect)
            {
                upgradeselect = true;
                TutorialManager.instance.CompleteStep();
            }
        }
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


