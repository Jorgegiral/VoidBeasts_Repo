using UnityEngine;

public class WallHP : MonoBehaviour
{
    [Header("HP Options")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float enemyDamage)
    {
        currentHealth-=enemyDamage;
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

}
