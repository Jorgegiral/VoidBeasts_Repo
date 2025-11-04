using System.Runtime.CompilerServices;
using UnityEngine;

public class Parcela : MonoBehaviour
{
    bool isFull;
    public Plants plant;
    int dayCount;
    int nightCount;
    [SerializeField]Material plantedMaterial;
    [SerializeField]Material actualMaterial;
    private Renderer render;
    private GameObject tempPlant;
    private void Start()
    {
        render = GetComponent<Renderer>();
        if (DayNightSystem.Instance != null)
        {
            DayNightSystem.Instance.RegisterParcela(this);
        }
    }
    public bool PlantIsFull()
    {
        return plant != null;

    }
    public void Planted()
    {
       render.material = plantedMaterial;
       dayCount = plant.numDias;
       nightCount  = plant.numDias;
    }
    public void GrowedPlant()
    {
        if (nightCount == 0) { 
         tempPlant = Instantiate(plant.plantGameObject[0], transform.position, transform.rotation);
         }
        if (nightCount == 1)
        {
            Destroy(tempPlant);
           tempPlant = Instantiate(plant.plantGameObject[1], transform.position, transform.rotation);
        }
        if (nightCount == 2)
        {
            Destroy(tempPlant);
            tempPlant = Instantiate(plant.plantGameObject[2], transform.position, transform.rotation);
        }
    }
    public void UnPlanted()
    {
        if (dayCount == 0)
        {
            Destroy(tempPlant);
            MoneySystem.instance.AddMoney(plant.ganancias);
            plant = null;
            dayCount = 0;
            nightCount = 0;
            render.material = actualMaterial;
        }
    }
    public void DayCountdown()
    {
        dayCount--;
    }
    public void NightCountdown()
    {
        nightCount--;
    }

}
