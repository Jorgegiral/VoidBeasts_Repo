using UnityEngine;

public class WallHP : MonoBehaviour
{
    [Header("HP Options")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    Renderer rend; //jorge
    MaterialPropertyBlock mpb;
    Renderer[] renderers;
    [SerializeField] string damagePropertyName = "_DamageAmount";
    [SerializeField] string intensityPropertyName = "_IntensityStains";
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();//Jorge
        mpb = new MaterialPropertyBlock(); //Jorge
        currentHealth = maxHealth;
        UpgradeManager.instance.RegisterWall(gameObject);
    }

    public void TakeDamage(float enemyDamage)
    {
        currentHealth-=enemyDamage;
        UpdateHPWalls();
        if (currentHealth < 0)
        {
            Destroy(gameObject);
            UpgradeManager.instance.UnRegisterWall(gameObject);
        }
    }
    public void UpdateHPWalls()
    {
        float fill = currentHealth / maxHealth;
        fill = Mathf.Clamp01(fill);
        float damageAmount = (1f - fill) * 2;
        float IntensityStains = (1f - fill);
        foreach (var r in renderers)
        {
            r.GetPropertyBlock(mpb);
            mpb.SetFloat(damagePropertyName, damageAmount);
            mpb.SetFloat(intensityPropertyName, IntensityStains);
            r.SetPropertyBlock(mpb);
        }
    }
}
