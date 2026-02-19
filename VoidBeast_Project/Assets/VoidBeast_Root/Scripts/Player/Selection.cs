using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] GameObject selection;
    [SerializeField] bool layerPlants;

    private void Update()
    {
        if (DayNightSystem.Instance.isNight || !PlayerStats.instance.isActionMode)
        {
            selection.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerStats.instance.isActionMode)
        { 
        if (other.CompareTag("Player") && DayNightSystem.Instance.isDay)
        {
            selection.SetActive(true);
            PlayerStats.instance.playerisInside = true;
            if (layerPlants)
            {
                PlayerStats.instance.layerPlants = true;
            }
            else
            {
                PlayerStats.instance.layerPlants = false;
            }
        }
    }

}
    private void OnTriggerStay(Collider other)
    {
        if (PlayerStats.instance.isActionMode)
        {
            if (other.CompareTag("Player") && DayNightSystem.Instance.isDay)
            {
                selection.SetActive(true);
                PlayerStats.instance.playerisInside = true;
                if (layerPlants)
                {
                    PlayerStats.instance.layerPlants = true;
                }
                else
                {
                    PlayerStats.instance.layerPlants = false;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && DayNightSystem.Instance.isDay)
        {
            selection.SetActive(false);
            PlayerStats.instance.playerisInside = false;
        }
        if (layerPlants)
        {
            PlayerStats.instance.layerPlants = false;
        }
    }
}