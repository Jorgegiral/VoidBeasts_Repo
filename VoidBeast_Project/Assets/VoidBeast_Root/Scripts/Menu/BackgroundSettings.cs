using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSettings : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    private void Start()
    {
        if (Settings.Instance != null && Settings.Instance.menuSprite != null)
        {
            backgroundImage.sprite = Settings.Instance.menuSprite;
        }
    }


}
