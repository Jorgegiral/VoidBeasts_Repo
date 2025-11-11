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
    public int enemykilledCount;
    public DailyPowerUpsSO powerUpChosen;
    private int selectedIndex = -1;

    void Awake()
    {
        if (instance == null) { instance = this; }
        playerCurrentHealth = playerMaxHealth;
    }

    public void ApplyStats()
    {
        if (powerUpChosen.type.ToString() == "Gun Damage")
        {
            powerUpChosen.value += gunDamage;
        }
        if (powerUpChosen.type.ToString() == "Death Timer")
        {
            powerUpChosen.value -= deathTimer;
            if(deathTimer <= 0)
            {
                deathTimer = 0;
            }       
        }
        if (powerUpChosen.type.ToString() == "Health")
        {
            powerUpChosen.value += playerMaxHealth;
            powerUpChosen.value += playerCurrentHealth;
        }
        if (powerUpChosen.type.ToString() == "Health Regen")
        {
            powerUpChosen.value += healthRegen;
        }
        if (powerUpChosen.type.ToString() == "Speed")
        {
            powerUpChosen.value += playerSpeed;
        }
        powerUpChosen = null;
    }

}
