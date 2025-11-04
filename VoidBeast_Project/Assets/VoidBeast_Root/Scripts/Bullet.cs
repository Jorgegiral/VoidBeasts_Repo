using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed = 2f;
    private Vector3 moveDirection;

    private void Awake()
    {

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
        Instantiate(hitVFX, transform.position, Quaternion.identity);
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            enemyHP.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
