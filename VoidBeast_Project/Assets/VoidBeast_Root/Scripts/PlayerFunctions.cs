using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerFunctions : MonoBehaviour
{
    [Header("Shoot config")]
    [SerializeField] Transform shootPoint;
    [SerializeField] List<GameObject> bulletVFX = new List<GameObject>();
    private GameObject effectToSpawn;
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera buildCam;
    [SerializeField] GameObject SeedMenu;
    [SerializeField] RotateToPlayer rotateToPlayer;
    LayerMask layerInteractable;
    LayerMask plantsInteractable;
    private bool isShooting = false;

    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);
    private Animator anim; //Jorge
    [SerializeField] GameObject gun; //Jorge
    //[SerializeField] float gunTime = 0.5f;

    [SerializeField] bool actionMode = true;
    [SerializeField] private Image blackAndWhiteImage;


    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        effectToSpawn = bulletVFX[0];
        anim = GetComponent<Animator>(); //Jorge
        gun.SetActive(false); //Jorge
    }

    void Shoot()
    {
        if (isShooting) return; 

        isShooting = true; 
        //gun.SetActive(true); //Jorge
        anim.SetTrigger("Shoot"); //Jorge
        rotateToPlayer.RotateOnShoot();
        GameObject bulletVFX;
        bulletVFX = Instantiate(effectToSpawn,shootPoint.transform.position, transform.rotation);

        //StartCoroutine(GunDelay());
    }
    public void GunActived()
    {
        gun.SetActive(true);
    }

    public void GunDesactived()
    {
        gun.SetActive(false);
    }

    /*System.Collections.IEnumerator GunDelay()
    {
        yield return new WaitForSecondsRealtime(gunTime);
        gun.SetActive(false);
    }*/

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (blackAndWhiteImage.IsActive())
        {
            return;
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();
            }
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (blackAndWhiteImage.IsActive())
        {
            return;
        }
        if (!context.performed) return;

        Shoot();
    }
    public void SwitchMode(InputAction.CallbackContext context)
    {

    }
    public void EndShoot()
    {
        isShooting = false;
    }
}

