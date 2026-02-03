using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] GameObject holderImage;
    [SerializeField] Image holderFiller;


    [Header("Shoot config")]
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletVFX;
    [SerializeField] RotateToPlayer rotateToPlayer;
    [SerializeField] GameObject gun;
    private bool canShoot = true;

    [Header("Ray config")]
    private bool canRay = true;
    [SerializeField] Image RayPanel;
    [SerializeField] GameObject tornadoVFX;
    [Header("Melee config")]
    private bool canMelee = true;
    private bool canSpin = true;
    private float holdTimer;
    [SerializeField] Collider attackCollider; 
    [SerializeField] int comboIndex;
    [SerializeField] private bool isHolding;
    [SerializeField] private float holdThreshold = 2f;
    [SerializeField] Image SpinPanel;
    float attackComboTimer = 0f;
    float comboResetTimer = 2f;



    [Header("Bomb config")]
    [SerializeField] GameObject bombPrefab;
    private bool canBomb = true;
    private float minTime = 0.1f;
    private float maxTime = 1f;
    [SerializeField] Image BombPanel;


    [Header("Mine config")]
    private bool canMine = true;
    [SerializeField] GameObject minePrefab;
    [SerializeField] Transform minePoint;
    [SerializeField] Image MinePanel;


    [Header("Sounds")]
    [SerializeField] AudioClip shootSound;


    private Animator anim;
    private RectTransform holderRect;

    void Start()
    {
        anim = GetComponent<Animator>(); 
        gun.SetActive(false);
        holderRect = holderImage.GetComponent<RectTransform>();

    }
    private void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer > 0.2f)
            {
                holderImage.SetActive(true);
            }
            holderFiller.fillAmount += 0.5f * Time.deltaTime;
            holderRect.position = Mouse.current.position.ReadValue();
        }
        if (comboIndex > 1)
        {
            attackComboTimer += Time.deltaTime;

            if (attackComboTimer >= comboResetTimer)
            {
                comboIndex = 1;
                attackComboTimer = 0f;
            }
        }
    }
    void Shoot()
    {
        if (!UpgradeManager.instance.isGunUnlocked) return;
        if (!canShoot) return;
        rotateToPlayer.RotateOnShoot();
        //gun.SetActive(true); //Jorge
        anim.SetTrigger("Shoot"); //Jorge
        anim.SetTrigger("Attack");

        Instantiate(bulletVFX, shootPoint.transform.position, transform.rotation);
        Settings.instance.PlaySoundFXClip(shootSound, transform, 1f);
        StartCoroutine(ShootCooldown());
        //StartCoroutine(GunDelay());
    }
    void RayGun()
    {
        if (!UpgradeManager.instance.isRayUnlocked) return;
        if (!canRay) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("Shoot");
        anim.SetTrigger("Attack");

        StartCoroutine(RayCooldown());


    }
    void PlantMine()
    {
        if (!UpgradeManager.instance.isMineUnlocked) return;
        if (!canMine) return;
        anim.SetTrigger("Mine");
        anim.SetTrigger("Attack");
        GameObject tempMine;
        tempMine = Instantiate(minePrefab, minePoint.transform.position, transform.rotation);
        Destroy(tempMine,20f);
        StartCoroutine(MineCooldown());

    }
    void ThrowBomb()
    {
        if (!UpgradeManager.instance.isBombUnlocked) return;
        if (!canBomb) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("ThrowBomb");
        anim.SetTrigger("Attack");
        Vector3 bombHit = rotateToPlayer.GetLastHitPoint();
        LaunchBomb(bombHit);

        StartCoroutine(BombCooldown());


    }
    void MeleeAttack()
    {
        if (!canMelee) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("Melee");
        anim.SetInteger("ComboIndex", comboIndex);
        comboIndex++;
        if (comboIndex == 3) comboIndex = 0;
        anim.SetTrigger("Attack");
        StartCoroutine(MeleeCooldown());


    }
    void SpinAttack()
    {
        if (!UpgradeManager.instance.isSpinUnlocked) return;
        if (!canSpin) return;
        anim.SetTrigger("Spin");
        anim.SetTrigger("Attack");

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
        yield return StartCoroutine(Cooldown(MinePanel, PlayerStats.instance.mineCooldown, () =>
        {
            canMine = true;
        })
        );
    }
    IEnumerator BombCooldown()
    {
        canBomb = false;
        yield return StartCoroutine(Cooldown(BombPanel, PlayerStats.instance.bombCooldown, () =>
            {
                canBomb = true;
            })
        );
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
        yield return StartCoroutine(Cooldown(SpinPanel, PlayerStats.instance.spinCooldown, () =>
        {
            canSpin = true;
        })
        );
    }
    IEnumerator RayCooldown()
    {
        canRay = false;
        yield return StartCoroutine(Cooldown(RayPanel, PlayerStats.instance.rayGunCooldown, () =>
        {
            canRay = true;
        })
        );
    }
    IEnumerator Cooldown(Image panel, float cooldownTime, System.Action onFinish)
    {
        panel.fillAmount = 0f;
        float timer = 0f;

        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;
            panel.fillAmount = timer / cooldownTime;
            yield return null;
        }

        panel.fillAmount = 1f;
        onFinish?.Invoke();
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
        if (DayNightSystem.Instance.isDay) return;

        if (context.started)
        {
         //   if (UpgradeManager.instance.isSpinUnlocked)
        ///    {
                holderFiller.fillAmount = 0f;
                isHolding = true;
                holdTimer = 0f;
           // }
        }
        else if (context.canceled)
        {
            holderImage.SetActive(false);
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
    public void EndAttack()
    {
      //  anim.Play("Vacio", 1);
        anim.ResetTrigger("Attack");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            other.GetComponent<EnemyHP>().TakeDamage(PlayerStats.instance.meleeDamage);
        }
    }
}
