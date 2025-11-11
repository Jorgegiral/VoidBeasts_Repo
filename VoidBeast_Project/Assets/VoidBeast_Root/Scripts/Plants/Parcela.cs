using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class Parcela : MonoBehaviour
{
    bool isFull;
    public Plants plant;
    int dayCount;
    int nightCount;
    [SerializeField]Material plantedMaterial;
    [SerializeField]Material actualMaterial;
    [SerializeField] GameObject starsVFX;
    private GameObject tempVFX;
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
       tempVFX = Instantiate(starsVFX,transform.position,Quaternion.LookRotation(Vector3.up));
        Destroy(tempVFX, 1);
       dayCount = plant.numDias;
       nightCount  = plant.numDias;
    }
    public void GrowedPlant()
    {
        Vector3 yoffset = new Vector3(0, 0.3f, 0);
        if (nightCount == 0) {
           Destroy(tempPlant);
           tempPlant = Instantiate(plant.plantGameObject[0], transform.position + yoffset, transform.rotation);
            
         }
        if (nightCount == 1)
        {
           Destroy(tempPlant);
           tempPlant = Instantiate(plant.plantGameObject[1], transform.position + yoffset, transform.rotation);
        }
        if (nightCount == 2)
        {
            Destroy(tempPlant);
            tempPlant = Instantiate(plant.plantGameObject[2], transform.position + yoffset, transform.rotation);
           
        }
        tempPlant.transform.parent = transform;

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
    public void DestroyedPlant()
    {
            plant = null;
            dayCount = 0;
            nightCount = 0;
            render.material = actualMaterial;   
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
