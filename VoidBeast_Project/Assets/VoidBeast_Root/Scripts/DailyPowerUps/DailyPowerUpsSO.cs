using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyPowerUpsSO", menuName = "Scriptable Objects/DailyPowerUpsSO")]
public class DailyPowerUpsSO : ScriptableObject
{
    public PowerUpRarity rarityName;
    public Sprite raritySprite;
    public PowerUpType type;
    public string description;
    
}
 public enum PowerUpType {
    Health,
    HealthRegen,
    GunDamage,
    Speed,
    DeathTimer,
    }
public enum PowerUpRarity
{
    Common,
    Rare,
    Legendary
}