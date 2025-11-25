using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed = 2f;
    [SerializeField] Collider bulletCollider;


    private void Awake()
    {
        bulletCollider = GetComponent<Collider>();
        Destroy(gameObject, 5f);

    }
    private void Update()
    {
        if(shootSpeed != 0)
        {
            transform.position += transform.forward * shootSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Plant") && !other.gameObject.CompareTag("Confiner"))
        {
            GameObject hitVFXGameObject = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFXGameObject, 2f);
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
                enemyHP.TakeDamage(PlayerStats.instance.gunDamage);
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Plant") && !other.gameObject.CompareTag("Confiner"))
        { 
        GameObject hitVFXGameObject = Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(hitVFXGameObject, 2f);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            enemyHP.TakeDamage(PlayerStats.instance.gunDamage);
        }
        Destroy(gameObject);
        }
    }
}
