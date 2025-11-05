using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    private float regenTimer = 0f;
    [SerializeField] Transform spawnpoint;
    [SerializeField] Slider sliderHP;
    [SerializeField] float invincibilityDurationSeconds;
    bool isInvicible = false;
    float deathCountdown = 5f;
    [SerializeField] TMP_Text deathTimerText;
    [SerializeField] Image blackAndWhiteImage;

    void Start()
    {
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


        PlayerStats.instance.playerCurrentHealth -= damage;
            UpdateHPSlider();

        if (PlayerStats.instance.playerCurrentHealth <= 0)
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
        PlayerStats.instance.playerCurrentHealth += heal;
        if(PlayerStats.instance.playerCurrentHealth > PlayerStats.instance.playerMaxHealth)
        {
            PlayerStats.instance.playerCurrentHealth = PlayerStats.instance.playerMaxHealth;
        }
        UpdateHPSlider();
    }
    public void UpdateHPSlider()
    {
        sliderHP.value = PlayerStats.instance.playerCurrentHealth;
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

            if (PlayerStats.instance.playerCurrentHealth < PlayerStats.instance.playerMaxHealth)
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
        if (!PlayerStats.instance.isDeath) return;

        deathCountdown -= Time.deltaTime;
        deathTimerText.text = deathCountdown.ToString("0");
        if (deathCountdown <= 0f)
        {
            if (spawnpoint != null)
            {
                transform.position = spawnpoint.position;
                Debug.Log("Spawning");
                deathTimerText.gameObject.SetActive(false);
                blackAndWhiteImage.gameObject.SetActive(false);

            }

            PlayerStats.instance.playerCurrentHealth = PlayerStats.instance.playerMaxHealth;
            UpdateHPSlider();
            PlayerStats.instance.isDeath = false; 
        }
    }


}
