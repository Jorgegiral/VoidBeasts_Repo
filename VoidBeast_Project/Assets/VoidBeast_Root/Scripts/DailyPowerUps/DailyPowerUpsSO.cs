using UnityEngine;
using UnityEngine.Localization.Tables;

[CreateAssetMenu(fileName = "DailyPowerUpsSO", menuName = "Scriptable Objects/DailyPowerUpsSO")]
public class DailyPowerUpsSO : ScriptableObject
{
    public PowerUpRarity rarityName;
    public Sprite raritySprite;
    public Sprite iconSprite;
    public PowerUpType type;
    public string description;
    public float value;
    public StringTableEntry descriptionTable;
}
 public enum PowerUpType {
    Health,
    HealthRegen,
    GunDamage,
    PlayerSpeed,
    Resurrection,
    FireRate,
    ExplosionDamage,
    WindDamage,
    AvailablePoints
    }
public enum PowerUpRarity
{
    Common,
    Rare,
    Legendary,
    Void
}