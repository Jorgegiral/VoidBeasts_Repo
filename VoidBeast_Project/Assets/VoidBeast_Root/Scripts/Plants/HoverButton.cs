using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler

{
    [Header("General Refs")]
    [SerializeField] GameObject etiqueta;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] bool isPlant;
    [Header("Plants Refs")]
    [SerializeField] TMP_Text earningsText;
    [SerializeField] TMP_Text daysText;
    [SerializeField] Plants plantToWatch;
    [Header("Build Refs")]
    [SerializeField] TMP_Text infoText;
    [SerializeField] TypeBuild buildingToWatch;




    private void UpdateText()
    {
        if ((isPlant))
        {
            nameText.text = plantToWatch.plantName;
            costText.text = plantToWatch.precio.ToString();
            daysText.text = plantToWatch.numDias.ToString();
            earningsText.text = plantToWatch.ganancias.ToString();
        }
        else
        {
            nameText.text = buildingToWatch.nameBuild;
            costText.text = buildingToWatch.precio.ToString();
            infoText.text = buildingToWatch.info;

            //build
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
