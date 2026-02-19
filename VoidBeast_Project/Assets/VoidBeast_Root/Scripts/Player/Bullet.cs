using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed = 2f;


    private void Awake()
    {
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
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Plant") ||
            otherGO.CompareTag("Confiner") ||
            otherGO.CompareTag("Wall") ||
            otherGO.CompareTag("Tower"))
            return;

        if (hitVFX != null)
        {
            GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (otherGO.CompareTag("Enemy") &&
            otherGO.TryGetComponent(out EnemyHP enemyHP))
        {
            enemyHP.TakeDamage(PlayerStats.instance.gunDamage);
        }
        Destroy(gameObject);
        }
    }

