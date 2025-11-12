using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Botones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler,IDeselectHandler
{
    [SerializeField] private float moveDistance = 15f;     
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] AudioClip moveSound;
    [SerializeField] AudioClip unClickSound;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private RectTransform rectTransform;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        targetPosition = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * moveSpeed);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
        Settings.instance.PlaySoundFXClip(moveSound, transform, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPosition = originalPosition;
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

    }
    public void OnSelect(BaseEventData eventData)
    {
        targetPosition = originalPosition + new Vector3(moveDistance, 0, 0);
        Settings.instance.PlaySoundFXClip(moveSound, transform, 1f);

    }

    public void OnDeselect(BaseEventData eventData)
    {
        targetPosition = originalPosition;
        Settings.instance.PlaySoundFXClip(unClickSound, transform, 1f);

    }
}
