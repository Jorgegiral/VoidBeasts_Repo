using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFunctions : MonoBehaviour
{
    [Header("Shoot config")]
    [SerializeField] Transform shootPoint;
    [SerializeField] List<GameObject> bulletVFX = new List<GameObject>();
    private GameObject effectToSpawn;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera buildCam;
    [SerializeField] GameObject SeedMenu;

    LayerMask layerInteractable;
    LayerMask plantsInteractable;

    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);


    [SerializeField] bool actionMode = true;
     
    
    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        effectToSpawn = bulletVFX[0];

    }

    void Shoot()
    {

            GameObject bullet = Instantiate(effectToSpawn, shootPoint.position, Quaternion.identity);

        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position - originRaycast, transform.TransformDirection(Vector3.forward), out hit, 30, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();
            }
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Shoot();
    }
    public void SwitchMode(InputAction.CallbackContext context)
    {

    }
}

