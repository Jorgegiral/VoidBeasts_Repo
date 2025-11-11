using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    float enemyMaxHealth;
    float enemyCurrentHealth;
    [SerializeField] private AudioClip deathEnemySound;
    [SerializeField] private GameObject smokeVFX;
    private void Awake()
    {
        enemyMaxHealth = ScaleEnemyHP();
        enemyCurrentHealth = enemyMaxHealth;
    }


    int ScaleEnemyHP()
    {
        int enemyHP =  DayNightSystem.Instance.nightNumber * 2 + 5;
        return enemyHP;
    }
    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        //PONER CAPA ROJA PARA FEEDBACK DE DAÑO
        if (enemyCurrentHealth < 0) 
        {
            BasicEnemy enemy = GetComponent<BasicEnemy>();
            GameObject tempSmoke = Instantiate(smokeVFX,transform.position,transform.rotation);
            Destroy(tempSmoke,1f);
            PlayerStats.instance.enemykilledCount++;
            if (enemy != null)
            {
                Settings.instance.PlaySoundFXClip(deathEnemySound, transform, 1f);

                enemy.OnDeath();

            }
            else
            {
                Settings.instance.PlaySoundFXClip(deathEnemySound, transform, 1f);

                Destroy(gameObject);
            }
        }
    }
}
