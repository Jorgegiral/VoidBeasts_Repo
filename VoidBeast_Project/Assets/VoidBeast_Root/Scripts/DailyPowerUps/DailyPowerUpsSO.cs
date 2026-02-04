using UnityEngine;

[CreateAssetMenu(fileName = "DailyPowerUpsSO", menuName = "Scriptable Objects/DailyPowerUpsSO")]
public class DailyPowerUpsSO : ScriptableObject
{
    public PowerUpRarity rarityName;
    public Sprite raritySprite;
    public PowerUpType type;
    public string description;
    public float value;
    
}
 public enum PowerUpType {
    Health,
    HealthRegen,
    GunDamage,
    PlayerSpeed,
    Resurrection,
    FireRate,
    ExplosionDamage,
    MeleeDamage,
    }
public enum PowerUpRarity
{
    Common,
    Rare,
    Legendary
}