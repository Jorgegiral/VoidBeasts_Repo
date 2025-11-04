using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public float playerSpeed;
    public float playerHealth;
    public bool isDeath;
    void Awake()
    {
        if (instance == null) { instance = this; }

    }

    
    void Update()
    {
        
    }
    
}
