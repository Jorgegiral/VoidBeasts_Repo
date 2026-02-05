using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [Header("Enemy Health Options")]
    [SerializeField] float minHealth;
    [SerializeField] float maxHealth;
    [SerializeField] private Image healthbar;
    [SerializeField] private GameObject healthob;
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
        UpdateHealthBar();
    }
    private void LateUpdate()
       {
           healthob.transform.rotation = Quaternion.LookRotation(healthob.transform.position - Camera.main.transform.position);
       }
    float ScaleEnemyHP()
    {
        enemyMaxHealth += DayNightSystem.Instance.nightNumber * 2;
        return enemyMaxHealth;
    }
    public void TakeDamage(float damage)
    {
        healthob.SetActive(true);
        enemyCurrentHealth -= damage;
        rend.material = damageMaterial;
        StartCoroutine(TakeDamageMaterial());
        UpdateHealthBar();
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


    void UpdateHealthBar()
    {
        float health = enemyCurrentHealth / enemyMaxHealth;
        health = Mathf.Clamp01(health);
        if (healthbar != null)
        {
            healthbar.fillAmount = health;
        }
    }
}
