using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BuildingHP : MonoBehaviour
{
    [SerializeField] float buildHP;
    [SerializeField] float currentBuildHP;
    [SerializeField] Canvas deathCanvas;
    public float attackRadius = 3f;
    [SerializeField] Image fillImage;
    [SerializeField] public GameObject imageHP;
    [SerializeField] AudioClip deathSound;
    private int enemyCounter = 0;
    [SerializeField] GameObject vfxRepair;
    Renderer rend; //jorge
    MaterialPropertyBlock mpb;
    Renderer[] renderers;

    [SerializeField] string damagePropertyName = "_DamageAmount";
    [SerializeField] string intensityPropertyName = "_IntensityStains";
    private void Awake()
    {      
        renderers = GetComponentsInChildren<Renderer>();//Jorge
        mpb = new MaterialPropertyBlock(); //Jorge

        deathCanvas.gameObject.SetActive(false);
        imageHP.SetActive(false);
        currentBuildHP = buildHP;

    }

    private void Update()
    {
        UpdateHP();
    }
    public void NewDayHealth()
    {
        GameObject vfxRepairobj = Instantiate(vfxRepair, transform.position, transform.rotation);
        Destroy(vfxRepairobj,5f);
        currentBuildHP = buildHP;
        UpdateHP();
    }
    public void TakeDamage(float damage)
    {
        currentBuildHP -= damage;
        UpdateHP();
        if (currentBuildHP <= 0 )
        {
            Settings.instance.PlaySoundFXClip(deathSound, transform, 1f);

            deathCanvas.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }

    }
    public Vector3 GetAttackPointInfinite(Vector3 enemyPosition)
    {
        enemyCounter++;

        Vector3 dir = (enemyPosition - transform.position);
        dir.y = 0f;

        float baseAngle = Mathf.Atan2(dir.z, dir.x);

        float angleStep = 0.5f;

        int side = enemyCounter % 2 == 0 ? 1 : -1;
        float ringOffset = (enemyCounter / 2) * angleStep;

        float finalAngle = baseAngle + side * ringOffset;

        Vector3 offset = new Vector3(
            Mathf.Cos(finalAngle),
            0f,
            Mathf.Sin(finalAngle)
        ) * attackRadius;

        Vector3 worldPoint = transform.position + offset;

        if (NavMesh.SamplePosition(worldPoint, out NavMeshHit hit, 1.5f, NavMesh.AllAreas))
            return hit.position;

        return worldPoint;
    }

    public void UpdateHP()
    {
        float fill = currentBuildHP / buildHP;
        fill = Mathf.Clamp01(fill);

        if (fillImage != null)
            fillImage.fillAmount = fill;
        //codigo Jorge:
        float damageAmount = (1f - fill) * 2;
        float IntensityStains = (1f - fill);
        /*
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat(damagePropertyName, damageAmount);
        rend.SetPropertyBlock(mpb);*/
        foreach (var r in renderers)
        {
            r.GetPropertyBlock(mpb);
            mpb.SetFloat(damagePropertyName, damageAmount);
            mpb.SetFloat(intensityPropertyName, IntensityStains);
            r.SetPropertyBlock(mpb);
        }
    }
}

