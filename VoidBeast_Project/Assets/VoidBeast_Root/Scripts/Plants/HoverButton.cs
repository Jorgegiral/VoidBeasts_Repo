using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler

{
    [SerializeField] GameObject etiqueta;
    [SerializeField] Plants plantToWatch;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text daysText;
    [SerializeField] TMP_Text earningsText;

    private void UpdateText()
    {
        nameText.text = plantToWatch.plantName;
        costText.text = plantToWatch.precio.ToString();
        daysText.text = plantToWatch.numDias.ToString();
        earningsText.text = plantToWatch.ganancias.ToString();

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
