using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler

{
    [Header("General Refs")]
    [SerializeField] GameObject etiqueta;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text lifeText;
    [SerializeField] bool isPlant;
    [Header("Plants Refs")]
    [SerializeField] TMP_Text earningsText;
    [SerializeField] TMP_Text daysText;
    [SerializeField] Plants plantToWatch;
    [Header("Build Refs")]
    [SerializeField] TMP_Text infoText;
    [SerializeField] TypeBuild buildingToWatch;
    [SerializeField] TMP_Text available;




    private void UpdateText()
    {
        if ((isPlant))
        {
            nameText.text = plantToWatch.plantName;
            costText.text = plantToWatch.precio.ToString();
            daysText.text = plantToWatch.numDias.ToString();
            lifeText.text = plantToWatch.life.ToString();
            earningsText.text = plantToWatch.ganancias.ToString();
        }
        else
        {
            nameText.text = buildingToWatch.nameBuild.GetLocalizedString();
            costText.text = buildingToWatch.precio.ToString();
            infoText.text = buildingToWatch.info.GetLocalizedString();
            UpdateAvailable();
            //build
        }

    }
    private void UpdateAvailable()
    {
        if(buildingToWatch.type == TypeBuild.BuildType.Wall)
        {
            available.text = UpgradeManager.instance.wallBought +"/"+ UpgradeManager.instance.maxWall;
        }
        if (buildingToWatch.type == TypeBuild.BuildType.Tower)
        {
            available.text = UpgradeManager.instance.towerBought + "/" + UpgradeManager.instance.maxTower;
        }
        if (buildingToWatch.type == TypeBuild.BuildType.Build)
        {
            available.text = UpgradeManager.instance.cropsBought + "/" + UpgradeManager.instance.maxCrop;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateText();

        etiqueta.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        etiqueta.SetActive(false);

    }



    public void OnSelect(BaseEventData eventData)
    {
        UpdateText();

        etiqueta.SetActive(true);
    }
}
