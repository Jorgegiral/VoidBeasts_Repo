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
    [SerializeField] GameObject rayVFX;
    [SerializeField] Transform rayPoint;
    [SerializeField] Image RayPanel;
    [SerializeField] GameObject tornadoVFX;
    [SerializeField] Transform nadoPoint;

    [Header("WindAttacks config")]
    private bool canDash = true;
    private bool canSpin = true;
    private float holdTimer;
    [SerializeField] Collider attackCollider; 
    [SerializeField] private bool isHolding;
    [SerializeField] private float holdThreshold = 2f;
    [SerializeField] Image SpinPanel;



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
    [SerializeField] AudioClip beamSound;
    [SerializeField] AudioClip spinSound;


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
    }
    void Shoot()
    {
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
        if (!SkillManager.instance.isMidTwoUnlocked) return;
        if (!canRay) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetBool("Ray", true);
        anim.SetTrigger("Attack");
        GameObject tempRay = Instantiate(rayVFX, rayPoint.transform.position, rayPoint.rotation,transform);
        Settings.instance.PlaySoundFXClip(beamSound, transform, 6f);
        Destroy(tempRay, 3f);
        StartCoroutine(DisableRayAfterTime(3f));
        StartCoroutine(RayCooldown());


    }
    void PlantMine()
    {
        if (!SkillManager.instance.isLeftOneUnlocked) return;
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
        if (!SkillManager.instance.isLeftTwoUnlocked) return;
        if (!canBomb) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("ThrowBomb");
        anim.SetTrigger("Attack");
        Vector3 bombHit = rotateToPlayer.GetLastHitPoint();
        LaunchBomb(bombHit);

        StartCoroutine(BombCooldown());


    }
    void Dash()
    {
        if (!SkillManager.instance.isRightOneUnlocked) return;
        if (!canDash) return;
        rotateToPlayer.RotateOnShoot();
        anim.SetTrigger("Dash");
        anim.SetTrigger("Attack");
        StartCoroutine(DashCooldown());

    }
    void SpinAttack()
    {
        if (!SkillManager.instance.isRightTwoUnlocked) return;
        if (!canSpin) return;
        anim.SetBool("isSpin",true);
        anim.SetTrigger("Attack");
        GameObject tempNado = Instantiate(tornadoVFX, nadoPoint.transform.position, nadoPoint.rotation, transform);
        Destroy(tempNado, 6f);
        Settings.instance.PlaySoundFXClip(spinSound, transform, 6f);
        StartCoroutine(DisableSpinAfterTime(6f));
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
    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(PlayerStats.instance.dashCooldown);
        canDash = true;
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
    IEnumerator DisableSpinAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isSpin", false);
    }
    IEnumerator DisableRayAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("Ray", false);
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
        panel.fillAmount = 1f;
        float timer = 0f;

        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;
            panel.fillAmount = 1f - (timer / cooldownTime);
            yield return null;
        }

        panel.fillAmount = 0f;
        onFinish?.Invoke();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (DayNightSystem.Instance.isDay) return;
        if (PlayerStats.instance.isDeath) return;
        if (context.started)
        {
            if (SkillManager.instance.isMidTwoUnlocked)
            {
                holderFiller.fillAmount = 0f;
                isHolding = true;
                holdTimer = 0f;
            }
        }
        else if (context.canceled)
        {
            holderImage.SetActive(false);
            isHolding = false;

            if (holdTimer >= holdThreshold)
            {
                RayGun();
            }
            else
            {
                Shoot();

            }
        }
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        if (PlayerStats.instance.isDeath) return;
        if (context.started)
        {
            if (SkillManager.instance.isRightTwoUnlocked)
            {
                holderFiller.fillAmount = 0f;
                isHolding = true;
                holdTimer = 0f;
           }
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
                Dash();
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
    IEnumerator SpeedBoost()
    {
        PlayerStats.instance.playerSpeed += 0.7f;
        yield return new WaitForSeconds(5f);
        PlayerStats.instance.playerSpeed -= 0.7f;
    }

}
