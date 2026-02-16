using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerFunctions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject escapeMenu;
    LayerMask layerInteractable;
    LayerMask layerPlant;
    LayerMask layerDestroyable;
    LayerMask layerWall;

    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);
    private Animator anim; //Jorge
    

    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        layerPlant = LayerMask.GetMask("Plant");
        layerDestroyable = LayerMask.GetMask("Destroyable");
        layerWall = LayerMask.GetMask("Wall");
        anim = GetComponent<Animator>(); //Jorge
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;
        if (!context.performed) return;
}


    public void OnEscapeButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if(!PlayerStats.instance.escapeMenuOpened)
        {
            escapeMenu.SetActive(true);
            PlayerStats.instance.menuOpened = true;

            PlayerStats.instance.escapeMenuOpened = true;
            Time.timeScale = 0f;
            Settings.instance.StopSingleSoundFX();
        }
        else
        {
            escapeMenu.SetActive(false);
            PlayerStats.instance.menuOpened = false;
            PlayerStats.instance.escapeMenuOpened = false;
            Time.timeScale = 1f;
        }
    }

}

