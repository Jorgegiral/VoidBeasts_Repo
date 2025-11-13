using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed = 2f;
    [SerializeField] Collider collider;

    private Vector3 moveDirection;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        moveDirection.y = 0f;
        Destroy(gameObject, 5f);

    }
    private void Update()
    {
        if(shootSpeed != 0)
        {
            transform.position += transform.forward * shootSpeed * Time.deltaTime;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Plant"))
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
        else
        {
            Physics.IgnoreCollision(other.collider, collider);
        }
    }
}
