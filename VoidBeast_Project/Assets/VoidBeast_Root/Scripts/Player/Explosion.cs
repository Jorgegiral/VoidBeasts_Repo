using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool isMine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            if(isMine)
            {
                enemyHP.TakeDamage(PlayerStats.instance.mineDamage);
            }
            else
            {
                enemyHP.TakeDamage(PlayerStats.instance.bombDamage);
            }
        }
    }
}
