using System.Collections;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    [Header("HP Options")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject VFXUpgrade;
    [SerializeField] GameObject VFXDestroy;


    Renderer rend; //jorge
    MaterialPropertyBlock mpb;
    Renderer[] renderers;
    [SerializeField] string damagePropertyName = "_DamageAmount";
    [SerializeField] string intensityPropertyName = "_IntensityStains";
    [SerializeField] AudioClip deathSound;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();//Jorge
        mpb = new MaterialPropertyBlock(); //Jorge
        currentHealth = maxHealth;
        StartCoroutine(RegisterCooldown());
    }

    public void TakeDamage(float enemyDamage)
    {
        currentHealth -= enemyDamage;
        if (currentHealth < 0)
        {
            Building area = GetComponent<Building>();
            if (area != null)
            {
                UpgradeManager.instance.UnRegisterTower(gameObject);
                area.Destroyed();
                GameObject destroyvfx = Instantiate(VFXDestroy, transform.position, transform.rotation);
                UpgradeManager.instance.towerAvailable++;
                UpgradeManager.instance.towerBought++;
                Settings.instance.PlayUniqueSoundSFXClip(deathSound, transform, 3f);
                Destroy(destroyvfx, 3f);
                Destroy(gameObject);
            }
        }
        UpdateHPTurrets();
    }
    public void UpgradeDamage(float upgradeDamage)
    {
        currentHealth -= upgradeDamage;
        if (currentHealth < 0)
        {
            UpgradeManager.instance.UnRegisterTower(gameObject);
            GameObject upgradevfx = Instantiate(VFXUpgrade, transform.position, transform.rotation);
            Destroy(upgradevfx, 1f);
            Destroy(gameObject);
        }

    }
    IEnumerator RegisterCooldown()
    {
        yield return new WaitForSeconds(1f);
        UpgradeManager.instance.RegisterTower(gameObject);
    }
    public void UpdateHPTurrets()
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
