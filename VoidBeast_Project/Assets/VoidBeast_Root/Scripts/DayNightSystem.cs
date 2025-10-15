using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightSystem : MonoBehaviour
{
    public int dayNumber;
    public int nightNumber;
    public bool isDay;
    public bool isNight;
    [SerializeField] TMP_Text dayNigtText;
    [SerializeField] Volume GlobalVolume;
    [SerializeField] Light globalLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayNumber = 1;
        nightNumber = 1;
    }

    
    void Update()
    {
        
    }
}
