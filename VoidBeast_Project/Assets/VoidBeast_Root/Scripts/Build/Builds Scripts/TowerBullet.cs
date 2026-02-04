using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] float shootSpeed = 2f;
    [SerializeField] int damage;

    private void Awake()
    {
        Destroy(gameObject, 5f);

    }
    private void Update()
    {
        if (shootSpeed != 0)
        {
            transform.position += transform.forward * shootSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject hitVFXGameObject = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFXGameObject, 2f);
                EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
                enemyHP.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("MainBuild"))
        {
            GameObject hitVFXGameObject = Instantiate(hitVFX, transform.position, Quaternion.identity);
            Destroy(hitVFXGameObject, 2f);
        }
    }
}
