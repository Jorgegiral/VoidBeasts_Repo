using NUnit.Framework.Internal;
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
    [SerializeField] GameObject starsVFX;
    [SerializeField] Shader desintegrate;
    [SerializeField] GameObject collectVFX;
    private GameObject tempVFX;
    private Renderer render;
    private GameObject tempPlant;
    [SerializeField] AudioClip plantSound;
    [SerializeField] AudioClip starSound;
    bool canPlant = false;
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
    public bool PlantIsFullandDayCount()
    {
        return plant != null && dayCount == 1;
    }
    public void Planted()
    {
       render.material = plantedMaterial;
       tempVFX = Instantiate(starsVFX,transform.position,Quaternion.LookRotation(Vector3.up));
        Settings.instance.PlaySoundFXClip(plantSound, transform, 1f);
        Settings.instance.PlaySoundFXClip(starSound, transform, 1f);

        //Jorge:
        if (TutorialManager.instance != null && TutorialManager.instance.step == 9)
        {
            if (!canPlant)
            {
                canPlant = true;
                TutorialManager.instance.CompleteStep();
            }
        }

        Destroy(tempVFX, 1);
       dayCount = plant.numDias;
       nightCount  = plant.numDias;
    }
    public void GrowedPlant()
    {
        Vector3 yoffset = new Vector3(0, 0.3f, 0);
        if (nightCount == 0) 
        {
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
            Renderer[] plantRenderer = tempPlant.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in plantRenderer)
            {
                for (int i = 0;i < renderer.materials.Length; i++)
                {
                    renderer.materials[i].shader = desintegrate;
                    renderer.materials[i].SetFloat("_DissolveAmount", 0f);
                }
                StartCoroutine(AnimateDissolve(renderer.materials, 3f));
            }

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
    private System.Collections.IEnumerator AnimateDissolve(Material[] materials, float duration)
    {
        yield return new WaitForSeconds(2f);
        tempVFX = Instantiate(collectVFX, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(tempVFX, 3f);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float dissolveValue = Mathf.Lerp(0f, 1f, elapsed / duration);
            foreach (var mat in materials)
            {
                mat.SetFloat("_DissolveAmount", dissolveValue);
            }
            yield return null;
        }

        foreach (var mat in materials)
        {
            mat.SetFloat("_DissolveAmount", 1f);
        }
        Destroy(tempPlant);
    }
}
