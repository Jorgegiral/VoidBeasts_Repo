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
    LayerMask layerDestroyable;
    LayerMask layerWall;

    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);
    private Animator anim; //Jorge
    [SerializeField] GameObject firstSelectedOnPause;
    [SerializeField] GameObject firstSelectedOnSeed; 
    bool parcelaSelection = false; //Jorge
    bool closeSeed = false;
    bool nightSelect = false;

    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        layerPlant = LayerMask.GetMask("Plant");
        layerDestroyable = LayerMask.GetMask("Destroyable");
        layerWall = LayerMask.GetMask("Wall");
        anim = GetComponent<Animator>(); //Jorge
        seedMenu.SetActive(false);
    }
    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.forward * 3, Color.red);

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;
        if (!context.performed) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();

                if ( TutorialManager.instance != null && TutorialManager.instance.step == 11)
                {
                    if (!nightSelect)
                    {
                        nightSelect = true;
                        TutorialManager.instance.CompleteStep();
                    }
                }
            }
        }

        if (DayNightSystem.Instance.isDay)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3, layerPlant))
            {
                ParcelaManager.instance.selectedParcela = hit.collider.GetComponent<ParcelaOrder>();
                seedMenu.SetActive(true);

                seedOpened = true;
                
            if (TutorialManager.instance.step == 7)
            {
                if (!parcelaSelection)
                {
                    if (!parcelaSelection)
                    {
                        parcelaSelection = true;
                        TutorialManager.instance.CompleteStep();
                    }
                }
            }
            if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward), out hit, 3, layerDestroyable))
            {
                TakeAreaEnviro areaEnviro = hit.collider.GetComponentInParent<TakeAreaEnviro>();
                areaEnviro.Destroyed();

                hit.collider.gameObject.SetActive(false);
            }
            if (Physics.Raycast(transform.position + new Vector3(0, 0.3f, 0), transform.TransformDirection(Vector3.forward), out hit, 3, layerWall))
            {
                WallBehaviour adaptWall = hit.collider.GetComponentInParent<WallBehaviour>();
                Building area = hit.collider.GetComponentInParent<Building>();
                area.Destroyed();
                adaptWall.ThrowRaycastNeighbours();
                adaptWall.gameObject.SetActive(false);
                UpgradeManager.instance.wallAvailable++;
            }
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
            //Jorge:

            if (TutorialManager.instance.step == 10)
            {
                if (!closeSeed)
                {
                    closeSeed = true;
                    TutorialManager.instance.CompleteStep();
                }
            }
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

}

