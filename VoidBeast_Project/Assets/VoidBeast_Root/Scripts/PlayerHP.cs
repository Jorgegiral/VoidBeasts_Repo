using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] float currentHealth;
    float maxHealth = 100;
    float healthRegen = 1f;
    float healthRegenTick = 1f;
    [SerializeField] Transform spawnpoint;
    [SerializeField] Slider sliderHP;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {

        }
    }
    public void HealDamage(float heal)
    {
        currentHealth += heal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void UpdateHPSlider()
    {
        sliderHP.value = currentHealth / maxHealth;
    }
    private void SpawnCharacter()
    {

    }
}
