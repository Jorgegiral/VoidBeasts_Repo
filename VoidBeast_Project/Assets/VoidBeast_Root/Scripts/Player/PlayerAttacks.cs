using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacks : MonoBehaviour
{
    [Header("Shoot config")]
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletVFX;
    [SerializeField] RotateToPlayer rotateToPlayer;
    [SerializeField] GameObject gun;
    private bool canShoot = true;

    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;


    private Animator anim;
    private GameObject effectToSpawn;

    void Start()
    {
        effectToSpawn = bulletVFX;
        anim = GetComponent<Animator>(); 
        gun.SetActive(false); 
    }
    void Shoot()
    {
        if (!canShoot) return;
        rotateToPlayer.RotateOnShoot();
        //gun.SetActive(true); //Jorge
        anim.SetTrigger("Shoot"); //Jorge
        GameObject bulletVFX;
        bulletVFX = Instantiate(effectToSpawn, shootPoint.transform.position, transform.rotation);
        Settings.instance.PlaySoundFXClip(shootSound, transform, 1f);
        StartCoroutine(ShootCooldown());
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


    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(PlayerStats.instance.gunAttackSpeed);
        canShoot = true;
    }
    public void OnShoot(InputAction.CallbackContext context)
    {

        if (PlayerStats.instance.isDeath) return;

        Shoot();
    }
}
