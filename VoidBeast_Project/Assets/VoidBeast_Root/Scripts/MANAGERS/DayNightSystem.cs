using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class DayNightSystem : MonoBehaviour
{
    public static DayNightSystem Instance;
    public int dayNumber;
    public int nightNumber;
    public bool isDay;
    public bool isNight;
    public bool startNight;
    public bool startDay;
    [SerializeField] TMP_Text dayNightText;
    [SerializeField] Volume globalVolume;
    [SerializeField] Light globalLight;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera nightCam;
    [SerializeField] Image[] DayNightIcons;
    [SerializeField] LocalizedString dayText;   
    [SerializeField] LocalizedString nightText;
    [SerializeField] Sprite[] dayNightSprites;
    [SerializeField] GameObject[] dayButton;
    float lightTransitionDuration = 3f;
    [SerializeField] float powerUpDelay = 3f;
    public List<Parcela> parcelas = new List<Parcela>();
    public List<ParcelaOrder> parcelasOrder = new List<ParcelaOrder>();
    //Jorge:
    bool enemies = false;

    public DailyPowerUP dailyPowerUP;
    public BuildingHP buildHP;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    void Start()
    {
        dayNumber = 1;
        nightNumber = 1;
        isDay = true;
        isNight = false;
        MusicManager.instance.PlayDaySong();
        UpdateDayNightUI();
    }



    private void UpdateDayNightUI()
    {
        string key = isDay ? "Day" : "Night";
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString("Tabla1", key);

        int number = isDay ? dayNumber : nightNumber;
        dayNightText.text = localizedString + " " + number;
    }
   
    public void ToDay()
    {
        if (isNight)
        {
            if (TutorialManager.instance != null && TutorialManager.instance.currentStep == TutorialManager.Step.KillEnemies)
            {
                if (!enemies)
                {
                    enemies = true;
                    TutorialManager.instance.CompleteStep();
                }
            }
            isDay = true;
            isNight = false;
            nightNumber++;
            StartCoroutine(ChangeLightTemperature(5000));
            playerCam.gameObject.SetActive(true);
            nightCam.gameObject.SetActive(false);
            ParcelaManager.instance.freeSeed = true;
            ParcelaManager.instance.freeSeedText.SetActive(true);
            MoneySystem.instance.UpdateMoneyText();
            buildHP.NewDayHealth();
            buildHP.imageHP.SetActive(false);
            dayButton[0].gameObject.SetActive(true);
            dayButton[1].gameObject.SetActive(true);
            dayButton[2].gameObject.SetActive(true);
            dayButton[3].gameObject.SetActive(true);
            DayNightIcons[0].sprite = dayNightSprites[3];
            DayNightIcons[1].sprite = dayNightSprites[2];
            StartCoroutine(StartPowerUp());
            foreach (ParcelaOrder p in parcelasOrder)
            {
                p.PlayRecolect();
            }
            foreach (Parcela p in parcelas)
            {
                if (p == null || p.plant == null) continue;
                p.DayCountdown();  
                p.UnPlanted();                 
            }
        }
        MusicManager.instance.PlayDaySong();
        UpdateDayNightUI();
    }
    public void ToNight()
    {
        if (isDay && PlayerStats.instance.isActionMode)
        {
            isDay = false;
            isNight = true;
            dayNumber++;
            StartCoroutine(ChangeLightTemperature(15000));
            playerCam.gameObject.SetActive(false);
            nightCam.gameObject.SetActive(true);
            buildHP.imageHP.SetActive(true);
            ParcelaManager.instance.freeSeedText.SetActive(false);
            dayButton[0].gameObject.SetActive(false);
            dayButton[1].gameObject.SetActive(false);
            dayButton[2].gameObject.SetActive(false);
            dayButton[3].gameObject.SetActive(false);
            DayNightIcons[0].sprite = dayNightSprites[1];
            DayNightIcons[1].sprite = dayNightSprites[0];
            foreach (Parcela p in parcelas)
            {
                if (p == null || p.plant == null) continue;
                p.NightCountdown();
                p.GrowedPlant();
            }
        }
        MusicManager.instance.PlayNightSong();
        UpdateDayNightUI();
    }

    public void RegisterParcela(Parcela newParcela)
    {
        if (!parcelas.Contains(newParcela))
        {
            parcelas.Add(newParcela);
        }
    }
    public void RegisterParcelaOrder(ParcelaOrder newParcela)
    {
        if (!parcelasOrder.Contains(newParcela))
        {
            parcelasOrder.Add(newParcela);
        }
    }
    public void dailyPowerUPPopUp()
    {
        dailyPowerUP.StartPowerUp();
    }

    IEnumerator ChangeLightTemperature(float targetTemperature)
    {
        float startTemp = globalLight.colorTemperature;
        float elapsed = 0f;

        var previousShadowMode = globalLight.shadows;
        globalLight.shadows = LightShadows.None; 

        while (elapsed < lightTransitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lightTransitionDuration;
            globalLight.colorTemperature = Mathf.Lerp(startTemp, targetTemperature, t);
            yield return null;
        }

        globalLight.colorTemperature = targetTemperature;
        globalLight.shadows = previousShadowMode; 
    }
    IEnumerator StartPowerUp()
    {
      yield return new WaitForSeconds(powerUpDelay);
      dailyPowerUPPopUp();
    }
}

