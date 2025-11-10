using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    int enemyMaxHealth;
    int enemyCurrentHealth;
    [SerializeField] private AudioClip damageEnemySound;
    [SerializeField] private GameObject smokeVFX;
    private void Awake()
    {
        enemyMaxHealth = ScaleEnemyHP();
        enemyCurrentHealth = enemyMaxHealth;
    }


    int ScaleEnemyHP()
    {
        int enemyHP =  DayNightSystem.Instance.nightNumber * 2;
        return enemyHP;
    }
    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        //PONER CAPA ROJA PARA FEEDBACK DE DAÑO
      //  Settings.instance.PlaySoundFXClip(damageEnemySound, transform, 1f);
        if (enemyCurrentHealth < 0) 
        {
            BasicEnemy enemy = GetComponent<BasicEnemy>();
            GameObject tempSmoke = Instantiate(smokeVFX,transform.position,transform.rotation);
            Destroy(tempSmoke,1f);
            PlayerStats.instance.enemykilledCount++;
            if (enemy != null)
            {
                enemy.OnDeath();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
