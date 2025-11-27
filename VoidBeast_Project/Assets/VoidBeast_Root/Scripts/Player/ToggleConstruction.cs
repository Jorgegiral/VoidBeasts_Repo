using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleConstruction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject constructionShop;
    [SerializeField] Grid showGrid;
    public PlayerInput playerInput;

    [Header("Test")]
    public bool isActionMode;
    public void SwitchMode(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (isActionMode)
        {
            playerInput.SwitchCurrentActionMap("BuildMode");
            constructionShop.SetActive(true);
            isActionMode = false;
        }
        else if (!isActionMode)
        {
            playerInput.SwitchCurrentActionMap("ActionMode");
            constructionShop.SetActive(false);
            isActionMode = true;

        }
    }
}

