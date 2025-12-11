using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class PlayerAttacks : MonoBehaviour
{
    [Header("Shoot config")]
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletVFX;
    [SerializeField] RotateToPlayer rotateToPlayer;
    [SerializeField] GameObject gun;
    private bool canShoot = true;

    [Header("Ray config")]
    private bool canRay = true;

    [Header("Melee config")]
    private bool canMelee = true;
    private bool canSpin = true;
    private float holdTimer;
    private bool isHolding;
    [SerializeField]private float holdThreshold = 3f;

    [Header("Bomb config")]
    [SerializeField] GameObject bombPrefab;
    private bool canBomb = true;
    private float minTime = 0.1f;
    private float maxTime = 1f;
    // public float forwardForce = 10f;   
    // public float upForce = 5f;

    [Header("Mine config")]
    private bool canMine = true;
    [SerializeField] GameObject minePrefab;
    [SerializeField] Transform minePoint;

    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;


    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); 
        gun.SetActive(false); 
    }
    private void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
        }
    }
    void Shoot()
    {
        if (!canShoot) return;
        rotateToPlayer.RotateOnShoot();
        //gun.SetActive(true); //Jorge
        anim.SetTrigger("Shoot"); //Jorge
        Instantiate(bulletVFX, shootPoint.transform.position, transform.rotation);
        Settings.instance.PlaySoundFXClip(shootSound, transform, 1f);
        StartCoroutine(ShootCooldown());
        //StartCoroutine(GunDelay());
    }
    void RayGun()
    {
        if (!canRay) return;
        rotateToPlayer.RotateOnShoot();
        StartCoroutine(RayCooldown());


    }
    void PlantMine()
    {
        if (!canMine) return;
        GameObject tempMine;
        tempMine = Instantiate(minePrefab, minePoint.transform.position, transform.rotation);
        Destroy(tempMine,20f);
        StartCoroutine(MineCooldown());

    }
    void ThrowBomb()
    {
        if (!canBomb) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("ThrowBomb");
        Vector3 bombHit = rotateToPlayer.GetLastHitPoint();
        LaunchBomb(bombHit);

        StartCoroutine(BombCooldown());


    }
    void MeleeAttack()
    {
        if (!canMelee) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("Melee");
        StartCoroutine(MeleeCooldown());


    }
    void SpinAttack()
    {
        if (!canSpin) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("Spin");
        StartCoroutine(SpinCooldown());


    }
    public void GunActived()
    {
        gun.SetActive(true);
    }

    public void GunDesactived()
    {
        gun.SetActive(false);
    }
    private void LaunchBomb(Vector3 destination)
    {
        float distance = Vector3.Distance(destination,shootPoint.position);
        Vector3 dir = destination - shootPoint.position;


        float time = Mathf.Lerp(minTime, maxTime, Mathf.InverseLerp(0, 12, distance));
        Vector3 dirXZ = new Vector3(dir.x,0,dir.z);
        Vector3 velocidadXZ = dirXZ / time;
        float velocidadY = (float)(dir.y +0.5f* 9.8 * time);
        
        GameObject bomb = Instantiate(bombPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(velocidadXZ.x, velocidadY, velocidadXZ.z);

    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(PlayerStats.instance.gunAttackSpeed);
        canShoot = true;
    }
    IEnumerator MineCooldown()
    {
        canMine = false;
        yield return new WaitForSeconds(PlayerStats.instance.mineCooldown);
        canMine = true;
    }
    IEnumerator BombCooldown()
    {
        canBomb = false;
        yield return new WaitForSeconds(PlayerStats.instance.bombCooldown);
        canBomb = true;
    }
    IEnumerator MeleeCooldown()
    {
        canMelee = false;
        yield return new WaitForSeconds(PlayerStats.instance.meleeAttackSpeed);
        canMelee = true;
    }
    IEnumerator SpinCooldown()
    {
        canSpin = false;
        yield return new WaitForSeconds(PlayerStats.instance.spinCooldown);
        canSpin = true;
    }
    IEnumerator RayCooldown()
    {
        canRay = false;
        yield return new WaitForSeconds(PlayerStats.instance.rayGunCooldown);
        canRay = true;
    }
    public void OnShoot(InputAction.CallbackContext context)
    {

        if (PlayerStats.instance.isDeath) return;

        Shoot();
    }
    public void OnRay(InputAction.CallbackContext context)
    {

        if (PlayerStats.instance.isDeath) return;

        Shoot();
    }
    public void OnMelee(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;

        if (context.started)
        {
            isHolding = true;
            holdTimer = 0f;
        }
        else if (context.canceled)
        {
            isHolding = false;

            if (holdTimer >= holdThreshold)
            {
                SpinAttack();
            }
            else
            {
                MeleeAttack();
            }
        }
    }
    public void OnBomb(InputAction.CallbackContext context)
    {

        if (PlayerStats.instance.isDeath) return;

        ThrowBomb();
    }
    public void OnMine(InputAction.CallbackContext context)
    {

        if (PlayerStats.instance.isDeath) return;

        PlantMine();
    }
}
