using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Enemy Health Options")]
    [SerializeField] float minHealth;
    [SerializeField] float maxHealth;
    float enemyMaxHealth;
    float enemyCurrentHealth;

    [Header("Enemy Sound/VFX Refs")]
    [SerializeField] private AudioClip deathEnemySound;
    [SerializeField] private GameObject smokeVFX;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Material baseMaterial;
    [SerializeField] Renderer rend;


    private void Awake()
    {
        enemyMaxHealth = Random.Range(minHealth, maxHealth);
        enemyMaxHealth = ScaleEnemyHP();
        EnemyManager.instance.Register(gameObject);

        enemyCurrentHealth = enemyMaxHealth;
    }

    float ScaleEnemyHP()
    {
        enemyMaxHealth += DayNightSystem.Instance.nightNumber * 2;
        return enemyMaxHealth;
    }
    public void TakeDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        rend.material = damageMaterial;
        StartCoroutine(TakeDamageMaterial());
        //PONER CAPA ROJA PARA FEEDBACK DE DAÑO
        if (enemyCurrentHealth < 0) 
        {
            BasicEnemy enemy = GetComponent<BasicEnemy>();
            GameObject tempSmoke = Instantiate(smokeVFX,transform.position,transform.rotation);
            Destroy(tempSmoke,1f);
            if (enemy != null)
            {
                Settings.instance.PlaySoundFXClip(deathEnemySound, transform, 1f);
                EnemyManager.instance.UnRegister(gameObject);

                enemy.OnDeath();

            }
            else
            {
                Settings.instance.PlaySoundFXClip(deathEnemySound, transform, 1f);
                EnemyManager.instance.UnRegister(gameObject);

                Destroy(gameObject);
            }
        }
    }
    IEnumerator TakeDamageMaterial()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        rend.material = baseMaterial;
    }
}
