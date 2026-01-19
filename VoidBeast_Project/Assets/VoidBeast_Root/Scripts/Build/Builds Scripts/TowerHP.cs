using System.Collections;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    [Header("HP Options")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(RegisterCooldown());
    }

    public void TakeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
            UpgradeManager.instance.UnRegisterTower(gameObject);
        }
    }

    IEnumerator RegisterCooldown()
    {
        yield return new WaitForSeconds(1f);
        UpgradeManager.instance.RegisterTower(gameObject);
    }
}
