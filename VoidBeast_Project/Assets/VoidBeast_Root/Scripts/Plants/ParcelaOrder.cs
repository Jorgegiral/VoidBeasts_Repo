using UnityEngine;

public class ParcelaOrder : MonoBehaviour
{
    public Parcela[] parcelas;
    [SerializeField] GameObject recolectVFX;
    private Collider parcelaCollider;
    public Collider barrierCollider;

    private void Start()
    {
        DayNightSystem.Instance.RegisterParcelaOrder(this);
        parcelaCollider = GetComponent<Collider>();

    }
    private void Update()
    {

            if (recolectVFX.activeSelf)
            {
                parcelaCollider.enabled = false;
                barrierCollider.enabled = true;

        }
        else
            {
                parcelaCollider.enabled = true;
                barrierCollider.enabled = false;

        }

    }
    public void PlayRecolect()
    {
        bool parcelasLlenas = false;
        foreach(Parcela p in parcelas)
        {
            if (p.PlantIsFullandDayCount())
            {
                parcelasLlenas = true;
                break;
            }
        }
        if (parcelasLlenas)
        {
            recolectVFX.SetActive(true);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (CompareTag("Grass"))
        {
            Destroy(collision.gameObject);

        }
    }
}
