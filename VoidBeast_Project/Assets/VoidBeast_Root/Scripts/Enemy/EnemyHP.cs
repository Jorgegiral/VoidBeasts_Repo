using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    int enemyMaxHealth;
    int enemyCurrentHealth;
    [SerializeField] private AudioClip damageEnemySound;
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
        Settings.instance.PlaySoundFXClip(damageEnemySound, transform, 1f);
        if (enemyCurrentHealth < 0) 
        {
            Destroy(gameObject);
        }
    }
}
