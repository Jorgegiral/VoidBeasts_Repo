using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] float currentHealth;
    float maxHealth = 100;
    float healthRegen = 1f;
    float healthRegenTick = 1f;
    private float regenTimer = 0f;
    [SerializeField] Transform spawnpoint;
    [SerializeField] Slider sliderHP;
    [SerializeField] float invincibilityDurationSeconds;
    bool isInvicible = false;
    float deathTimer = 5f;
    float deathCountdown;
    [SerializeField] TMP_Text deathTimerText;
    [SerializeField] Image blackAndWhiteImage;

    bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        deathTimerText.gameObject.SetActive(false);
        blackAndWhiteImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        Regeneration();
        DeathTimer();
    }
    public void TakeDamage(float damage)
    {
        if (isInvicible || isDead)
        {
            return;
        }

     
            currentHealth -= damage;
            UpdateHPSlider();

        if (currentHealth <= 0)
            {
            StartDeathTimer();
            return;
        }
            UpdateHPSlider();
            BecomeTemporarilyInvincible();
            isInvicible=false;
        
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
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvicible = true;
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvicible = false;
    }
    private void Regeneration()
    {
        if (isDead) return;

        regenTimer += Time.deltaTime;

        if (regenTimer >= healthRegenTick)
        {
            regenTimer = 0f;

            if (currentHealth < maxHealth)
            {
                HealDamage(healthRegen);
            }
        }
    }
    private void StartDeathTimer()
    {
        if (isDead) return;

        isDead = true;
        deathTimerText.gameObject.SetActive(true);
        blackAndWhiteImage.gameObject.SetActive(true);

        deathCountdown = deathTimer;
    }
    private void DeathTimer()
    {
        if (!isDead) return;

        deathCountdown -= Time.deltaTime;
        deathTimerText.text = deathCountdown.ToString("0");
        if (deathCountdown <= 0f)
        {
            if (spawnpoint != null)
            {
                transform.position = spawnpoint.position;
                deathTimerText.gameObject.SetActive(false);
                blackAndWhiteImage.gameObject.SetActive(false);

            }

            currentHealth = maxHealth;
            UpdateHPSlider();
            isDead = false; ;
        }
    }


}
