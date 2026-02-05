using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoverUpgrade : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [Header("General Refs")]
    [SerializeField] GameObject etiqueta;
    [SerializeField] Image etiquetaImg;
    [SerializeField] RectTransform etiquetaRect;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] TMP_Text costText;
    [SerializeField] string nameSkill;
    [SerializeField] string costSkill;
    [SerializeField] string infoSkill;
    private void Start()
    {
        etiquetaRect = etiquetaImg.GetComponent<RectTransform>();
        etiqueta.SetActive(false);
    }
    private void Update()
    {
        etiquetaRect.position = Mouse.current.position.ReadValue() + new Vector2(270, 130);
    }
    private void UpdateText()
    {
        nameText.text = nameSkill;
        costText.text = costSkill;
        infoText.text = infoSkill;
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
