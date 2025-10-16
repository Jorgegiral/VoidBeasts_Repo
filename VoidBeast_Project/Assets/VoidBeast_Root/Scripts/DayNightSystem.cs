using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightSystem : MonoBehaviour
{
    public static DayNightSystem Instance { get; private set; }
    public int dayNumber;
    public int nightNumber;
    public bool isDay;
    public bool isNight;
    public bool startNight;
    public int enemyQuantity;
    [SerializeField] TMP_Text dayNightText;
    [SerializeField]  Volume globalVolume;
    [SerializeField]  Light globalLight;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        
    }
    void Start()
    {
        dayNumber = 1;
        nightNumber = 1;
        isDay = true;
        isNight = false;
        
        UpdateDayNightUI();
    }

    void Update()
    {

    }

    private void UpdateDayNightUI()
    {
        if (dayNightText != null)
        {
            dayNightText.text = isDay ? $"Día {dayNumber}" : $"Noche {nightNumber}";
        }
    }
   
    public void ToDay()
    {
        if (isNight)
        {
            isDay = true;
            isNight = false;
            nightNumber++;
            globalLight.colorTemperature = 5000;
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

