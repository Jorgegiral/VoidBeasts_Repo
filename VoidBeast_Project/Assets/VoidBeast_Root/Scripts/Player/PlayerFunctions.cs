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
    [SerializeField] GameObject seedMenu;
    [SerializeField] GameObject escapeMenu;
    bool seedOpened = false;
    LayerMask layerInteractable;
    LayerMask layerPlant;
    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);
    private Animator anim; //Jorge
    [SerializeField] GameObject firstSelectedOnPause;
    [SerializeField] GameObject firstSelectedOnSeed;


    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        layerPlant = LayerMask.GetMask("Plant");

        anim = GetComponent<Animator>(); //Jorge
        seedMenu.SetActive(false);
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();
            }
        }
        if (DayNightSystem.Instance.isDay) { 
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, layerPlant))
        {
            ParcelaManager.instance.selectedParcela = hit.collider.GetComponent<ParcelaOrder>();
            seedMenu.SetActive(true);
                StartCoroutine(SelectFirstButtonDelayed());

                seedOpened = true;
        }
    }
}


    public void OnEscapeButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (seedOpened)
        {
            seedMenu.SetActive(false);
            seedOpened = false;
            PlayerStats.instance.blockMovement = false;
        }   
        else if(!PlayerStats.instance.menuOpened)
        {
            escapeMenu.SetActive(true);
            PlayerStats.instance.menuOpened = true;
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(firstSelectedOnPause);

        }
        else
        {
            escapeMenu.SetActive(false);
            PlayerStats.instance.menuOpened = false;
            Time.timeScale = 1f;
        }
    }

    private IEnumerator SelectFirstButtonDelayed()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedOnSeed);
    }
}

