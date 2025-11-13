using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public float playerSpeed;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public bool isDeath = false;
    public bool blockMovement = false;
    public float healthRegen;
    public float healthRegenTick;
    public float deathTimer = 5f;
    public float gunDamage;
    public float gunAttackSpeed = 2f;
    public int enemykilledCount;
    public bool menuOpened;
    public DailyPowerUpsSO powerUpChosen;
    private int selectedIndex = -1;

    void Awake()
    {
        if (instance == null) { instance = this; }
        playerCurrentHealth = playerMaxHealth;
    }

    public void ApplyStats()
    {
        if (powerUpChosen.type.ToString() == "GunDamage")
        {
            gunDamage += powerUpChosen.value;
        }
        if (powerUpChosen.type.ToString() == "DeathTimer")
        {
            deathTimer += powerUpChosen.value;
            if(deathTimer <= 0)
            {
                deathTimer = 0;
            }       
        }
        if (powerUpChosen.type.ToString() == "Resurrection")
        {
            deathTimer += powerUpChosen.value;
            if (deathTimer <= 0)
            {
                deathTimer = 0;
            }
        }
        if (powerUpChosen.type.ToString() == "Health")
        {
            playerMaxHealth += powerUpChosen.value;
            playerCurrentHealth += powerUpChosen.value;
        }
        if (powerUpChosen.type.ToString() == "HealthRegen")
        {
            healthRegen += powerUpChosen.value;
        }
        if (powerUpChosen.type.ToString() == "PlayerSpeed")
        {
            playerSpeed += powerUpChosen.value;
        }
        if (powerUpChosen.type.ToString() == "FireRate")
        {
            gunAttackSpeed += powerUpChosen.value;
            if (gunAttackSpeed <= 0)
            {
                gunAttackSpeed = 0;
            }
        }
        powerUpChosen = null;
    }

}
