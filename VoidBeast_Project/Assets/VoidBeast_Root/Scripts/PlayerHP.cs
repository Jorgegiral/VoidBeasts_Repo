using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] float currentHealth;
    private float regenTimer = 0f;
    [SerializeField] Transform spawnpoint;
    [SerializeField] Slider sliderHP;
    [SerializeField] float invincibilityDurationSeconds;
    bool isInvicible = false;
    float deathCountdown;
    [SerializeField] TMP_Text deathTimerText;
    [SerializeField] Image blackAndWhiteImage;

    void Start()
    {
        currentHealth = PlayerStats.instance.playerHealth;
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
        if (isInvicible || PlayerStats.instance.isDeath)
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
        if(currentHealth > PlayerStats.instance.playerHealth)
        {
            currentHealth = PlayerStats.instance.playerHealth;
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
        if (PlayerStats.instance.isDeath) return;

        regenTimer += Time.deltaTime;

        if (regenTimer >= PlayerStats.instance.healthRegenTick)
        {
            regenTimer = 0f;

            if (currentHealth < PlayerStats.instance.playerHealth)
            {
                HealDamage(PlayerStats.instance.healthRegen);
            }
        }
    }
    private void StartDeathTimer()
    {
        if (PlayerStats.instance.isDeath) return;

        PlayerStats.instance.isDeath = true;
        deathTimerText.gameObject.SetActive(true);
        blackAndWhiteImage.gameObject.SetActive(true);

        deathCountdown = PlayerStats.instance.deathTimer;
    }
    private void DeathTimer()
    {
        if (PlayerStats.instance.isDeath) return;

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

            currentHealth = PlayerStats.instance.playerHealth;
            UpdateHPSlider();
            PlayerStats.instance.isDeath = false; ;
        }
    }


}
