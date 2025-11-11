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



}
