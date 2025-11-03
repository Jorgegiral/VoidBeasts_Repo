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
    public int enemyQuantity;
    [SerializeField] TMP_Text dayNightText;
    [SerializeField] Volume globalVolume;
    [SerializeField] Light globalLight;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera nightCam;
    [SerializeField] Image[] DayNightIcons;
    [SerializeField] LocalizedString dayText;   
    [SerializeField] LocalizedString nightText;
    [SerializeField] Sprite[] DayNightSprites;
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
        DayNightIcons[0].sprite = DayNightSprites[0];
        DayNightIcons[1].sprite = DayNightSprites[0];
        DayNightIcons[2].sprite = DayNightSprites[3];

        UpdateDayNightUI();
    }

    void Update()
    {

    }

    private void UpdateDayNightUI()
    {
        string key = isDay ? "A008" : "A009";
        var localizedString = LocalizationSettings.StringDatabase.GetLocalizedString("Tabla1", key);

        int number = isDay ? dayNumber : nightNumber;
        dayNightText.text = localizedString + " " + number;
    }
   
    public void ToDay()
    {
        if (isNight)
        {
            isDay = true;
            isNight = false;
            nightNumber++;
            globalLight.colorTemperature = 5000;
            playerCam.gameObject.SetActive(true);
            nightCam.gameObject.SetActive(false);
            DayNightIcons[0].sprite = DayNightSprites[0];
            DayNightIcons[1].sprite = DayNightSprites[0];
            DayNightIcons[2].sprite = DayNightSprites[3];


        }

        UpdateDayNightUI();
    }
    public void ToNight()
    {
        if (isDay)
        {
            isDay = false;
            isNight = true;
            EnemyQuantityScale();
            dayNumber++;
            globalLight.colorTemperature = 15000;
            playerCam.gameObject.SetActive(false);
            nightCam.gameObject.SetActive(true);
            DayNightIcons[0].sprite = DayNightSprites[1];
            DayNightIcons[1].sprite = DayNightSprites[1];
            DayNightIcons[2].sprite = DayNightSprites[2];
        }
        UpdateDayNightUI();
    }
    public int EnemyQuantityScale()
    {
        //por ahora asi
        enemyQuantity = nightNumber * 5;
        return enemyQuantity;
    }
}

