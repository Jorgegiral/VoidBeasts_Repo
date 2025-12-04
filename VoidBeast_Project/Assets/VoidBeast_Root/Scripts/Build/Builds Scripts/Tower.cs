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
    private bool canShoot;

    private void Update()
    {
    }
    private void GetTarget()
    {
        Collider[] targetTransform = Physics.OverlapSphere(transform.position, range, enemyLayer);
        
    }
    private void ShootTarget()
    {
        if (!canShoot) return;
        if (target != null)
        {
            RotateToTarget();
            Instantiate(bulletVFX, cannonPoint.transform.position, transform.rotation);
            StartCoroutine(ShootCooldown());
        }

    }
    private void RotateToTarget()
    {
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
}
