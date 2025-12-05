using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] float range = 5f;
    [SerializeField] GameObject cannonPoint;
    [SerializeField] GameObject bulletVFX;
    [SerializeField] LayerMask enemyLayer;

    [Header("testers")]
    [SerializeField] GameObject target;
    [SerializeField] float shootCD = 2f;
    [SerializeField] bool enemyIsInSight;
    private bool canShoot = true;


    private void Start()
    {
        StartCoroutine(TargetScanner());

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
        target = null;

        foreach (var enemy in EnemyManager.instance.enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range)
            {
                target = enemy;
                enemyIsInSight = true;
                break;
            }
        }
        enemyIsInSight = target != null;
    }
    private void ShootTarget()
    {
        if (!canShoot) return;
        if (target != null)
        {
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
            yield return new WaitForSeconds(0.5f);
        }
    }
}
