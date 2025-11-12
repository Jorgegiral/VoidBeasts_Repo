using UnityEngine;

public class ParcelaOrder : MonoBehaviour
{
    public Parcela[] parcelas;
    [SerializeField] GameObject recolectVFX;
    private void Start()
    {
        DayNightSystem.Instance.RegisterParcelaOrder(this);

    }
    public void PlayRecolect()
    {
        bool parcelasLlenas = false;
        foreach(Parcela p in parcelas)
        {
            if (p.PlantIsFull())
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
}
