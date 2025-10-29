using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask enemyMask;
    [SerializeField] private int damage = 10;
    [SerializeField] GameObject hitVFX;
    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyHP = other.GetComponent<EnemyHP>();
            enemyHP.TakeDamage(damage);
            GameObject hit = Instantiate(hitVFX, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else
        {
            GameObject hit = Instantiate(hitVFX, transform.position, transform.rotation);
            
            Destroy(gameObject);
        }
    }
}
