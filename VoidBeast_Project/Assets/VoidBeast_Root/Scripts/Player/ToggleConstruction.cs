using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleConstruction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject constructionShop;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject buildCam;
    [SerializeField] GameObject table;

    public PlayerInput playerInput;

    [Header("Animation References")]
    private Animator anim;

    [Header("Test")]
    public bool isActionMode = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
        grid.SetActive(false);
    }
    public void TableActived()
    {
        table.SetActive(true);
    }

    public void TableDesactived()
    {
        table.SetActive(false);
    }
    public void SwitchMode(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (isActionMode)
        {
            playerInput.SwitchCurrentActionMap("BuildMode");
            constructionShop.SetActive(true);
            playerCam.SetActive(false);
            buildCam.SetActive(true);
            grid.SetActive(true);
            anim.SetBool("isBuilding", true);

            isActionMode = false;
        }
        else if (!isActionMode)
        {
            playerInput.SwitchCurrentActionMap("ActionMode");
            constructionShop.SetActive(false);
            playerCam.SetActive(true);
            buildCam.SetActive(false);
            grid.SetActive(false);
            anim.SetBool("isBuilding", false);

            isActionMode = true;

        }
    }

}

