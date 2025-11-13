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
    [SerializeField] GameObject seedMenu;
    [SerializeField] RotateToPlayer rotateToPlayer;
    [SerializeField] GameObject escapeMenu;
    [SerializeField] AudioClip shootSound;
    bool seedOpened = false;
    LayerMask layerInteractable;
    LayerMask layerPlant;
    private bool isShooting = false;

    private Vector3 originRaycast = new Vector3(0, 0.5f, 0);
    private Animator anim; //Jorge
    [SerializeField] GameObject gun; //Jorge
    //[SerializeField] float gunTime = 0.5f;

    [SerializeField] bool actionMode = true;


    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable");
        layerPlant = LayerMask.GetMask("Plant");

        effectToSpawn = bulletVFX[0];
        anim = GetComponent<Animator>(); //Jorge
        gun.SetActive(false); //Jorge
        seedMenu.SetActive(false);
    }

    void Shoot()
    {
        rotateToPlayer.RotateOnShoot();
        isShooting = true; 
        //gun.SetActive(true); //Jorge
        anim.SetTrigger("Shoot"); //Jorge
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
        if (PlayerStats.instance.isDeath) return;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, layerInteractable))
        {
            if (DayNightSystem.Instance != null && DayNightSystem.Instance.isDay)
            {
                DayNightSystem.Instance.ToNight();
            }
        }
        if (DayNightSystem.Instance.isDay) { 
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 6, layerPlant))
        {
            ParcelaManager.instance.selectedParcela = hit.collider.GetComponent<ParcelaOrder>();
            seedMenu.SetActive(true);
                seedOpened = true;
        }
    }
}
    public void OnShoot(InputAction.CallbackContext context)
    {

        if (isShooting) return;
        if (PlayerStats.instance.isDeath) return;

        Shoot();
        Settings.instance.PlaySoundFXClip(shootSound,transform, 1f);
    }
    public void SwitchMode(InputAction.CallbackContext context)
    {
        //Modo construcción
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
        }
        else
        {
            escapeMenu.SetActive(false);
            PlayerStats.instance.menuOpened = false;
            Time.timeScale = 1f;
        }
    }
    public void EndShoot()
    {
        isShooting = false;
    }
}

