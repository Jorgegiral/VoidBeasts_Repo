using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public float playerSpeed;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public bool isDeath = false;
    public bool isPlanting = false;
    public float healthRegen;
    public float healthRegenTick;
    public float deathTimer = 5f;

    void Awake()
    {
        if (instance == null) { instance = this; }
        playerCurrentHealth = playerMaxHealth;
    }

    
    
}
