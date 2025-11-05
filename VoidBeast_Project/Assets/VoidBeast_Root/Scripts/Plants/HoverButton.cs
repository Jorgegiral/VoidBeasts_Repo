using UnityEngine;
using UnityEngine.EventSystems;
public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject etiqueta;

    public void OnPointerEnter(PointerEventData eventData)
    {
        etiqueta.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        etiqueta.SetActive(false);

    }


}
