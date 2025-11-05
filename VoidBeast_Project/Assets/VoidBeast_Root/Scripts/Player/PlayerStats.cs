using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public float playerSpeed;
    public float playerHealth;
    public bool isDeath = false;
    public bool isPlanting = false;
    public float healthRegen;
    public float healthRegenTick;
    public float deathTimer = 5f;
    public int gunDamage;

    void Awake()
    {
        if (instance == null) { instance = this; }

    }

    
    
}
