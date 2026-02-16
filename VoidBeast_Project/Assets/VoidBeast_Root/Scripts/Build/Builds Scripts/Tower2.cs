using System.Collections;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;

public class Tower2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] float range = 5f;
    [SerializeField] GameObject cannonPoint;
    [SerializeField] GameObject bulletVFX;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] AudioClip towerShoot;

    [Header("testers")]
    [SerializeField] GameObject target;
    [SerializeField] float shootCD = 0.8f;
    [SerializeField] bool enemyIsInSight;
    private bool canShoot = true;
    private Animator anim;
    private void Start()
    {
        StartCoroutine(TargetScanner());
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        CheckEnemy();
    }
    private void CheckEnemy()
    {
        if (target == null)
        {
            enemyIsInSight = false;
            return;
        }

        if (enemyIsInSight)
        {
            RotateToTarget();
            ShootTarget();
        }

    }
    private void GetTarget()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist <= range)
            {
                enemyIsInSight = true;
                return;
            }
            else
            {
                target = null;
                enemyIsInSight = false;
            }
        }
        foreach (var enemy in EnemyManager.instance.enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                target = enemy;
                enemyIsInSight = true;
                return;
            }
        }

        enemyIsInSight = false;
    }
    private void ShootTarget()
    {
        if (!canShoot) return;
        if (target != null)
        {
            Settings.instance.PlayUniqueSoundSFXClip(towerShoot, transform, 3f);
            anim.SetTrigger("Shoot");
            Instantiate(bulletVFX, cannonPoint.transform.position, transform.rotation);
            StartCoroutine(ShootCooldown());
        }

    }
    private void RotateToTarget()
    {
        if (target == null) return;

        transform.LookAt(target.transform);
    }
void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    IEnumerator MachineGun()
    {
        yield return new WaitForSeconds(0.2f);
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
    IEnumerator TargetScanner()
    {
        while (true)
        {
            GetTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }

}
