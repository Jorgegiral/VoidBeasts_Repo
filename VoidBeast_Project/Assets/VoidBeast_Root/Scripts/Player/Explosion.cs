using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            enemyHP.TakeDamage(PlayerStats.instance.explosionDamage);

        }
    }
}
