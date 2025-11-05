using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public float playerSpeed;
    public float playerHealth;
    public bool isDeath = false;
    public bool isPlanting = false;
    void Awake()
    {
        if (instance == null) { instance = this; }

    }

    
    
}
