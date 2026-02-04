using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] GameObject selection;
    [SerializeField] bool layerPlants;

    private void OnTriggerEnter(Collider other)
    {
        if(GridBuilding.instance.buildingTemp == null) 
        { 
        if (other.CompareTag("Player"))
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
        if (other.CompareTag("Player"))
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