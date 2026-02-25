using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    [Header("Player General Stats")]

    public float playerSpeed;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public bool isDeath = false;
    public bool blockMovement = false;
    public float healthRegen;
    public float healthRegenTick;
    public float deathTimer = 5f;

    [Header("Player Damage Stats")]
    public float gunDamage;
    public float explosionDamage;
    public float windDamage;


    [Header("Player Cooldown Stats")]

    public float gunAttackSpeed = 2f;
    public float rayGunCooldown = 10f;
    public float dashCooldown = 5f;
    public float spinCooldown = 5f;
    public float mineCooldown = 5f;
    public float bombCooldown = 8f;

    [Header("Stats Functions")]
    public int enemykilledCount;
    public bool menuOpened;
    public bool escapeMenuOpened;

    public DailyPowerUpsSO powerUpChosen;
    public bool isActionMode = true;
    public bool playerisInside;
    public bool layerPlants;
    public bool isDashing;

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
        if (powerUpChosen.type.ToString() == "ExplosionDamage")
        {
            explosionDamage += powerUpChosen.value;
        }
        if (powerUpChosen.type.ToString() == "windDamage")
        {
            windDamage += powerUpChosen.value;
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
            if (gunAttackSpeed <= 0.1f)
            {
                gunAttackSpeed = 0.1f;
            }
        }
        if (powerUpChosen.type.ToString() == "AvailablePoints")
        {
            SkillManager.instance.freeUpgrade = true;
            SkillManager.instance.maxPoints--;
            SkillManager.instance.AddPoint();
            UpgradeManager.instance.TakeUpgrade();
        }
        powerUpChosen = null;
    }

}
