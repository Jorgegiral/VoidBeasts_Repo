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
    [SerializeField] float invincibilityDurationSeconds;
    bool isInvicible = false;
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (isInvicible)
        {
            return;
        }
        else
        {

            currentHealth -= damage;
            if (currentHealth < 0)
            {

            }
            UpdateHPSlider();
            BecomeTemporarilyInvincible();
            isInvicible=false;
        }
    }
    public void HealDamage(float heal)
    {
        currentHealth += heal;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHPSlider();
    }
    public void UpdateHPSlider()
    {
        sliderHP.value = currentHealth;
    }
    void BecomeTemporarilyInvincible()
    {
        float deltaTime = Time.deltaTime;
        for (float i = 0; i < invincibilityDurationSeconds; i += deltaTime)
        {
            isInvicible = true;
        }
    }

}
