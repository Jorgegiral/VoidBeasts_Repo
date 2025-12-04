using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Collider range;
    [SerializeField] GameObject cannonPoint;
    [SerializeField] GameObject bulletVFX;
    [Header("testers")]
    [SerializeField] Transform target;
    [SerializeField] float shootCD = 2f;
    private bool canShoot;
    

    private void GetTarget()
    {
        if(target == null)
        {
            
        }
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
        transform.LookAt(target);
    }
    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
}
