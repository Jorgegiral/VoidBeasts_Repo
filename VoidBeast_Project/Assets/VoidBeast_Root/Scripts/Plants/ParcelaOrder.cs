using Unity.VisualScripting;
using UnityEngine;

public class ParcelaOrder : MonoBehaviour
{
    public Parcela[] parcelas;
    [SerializeField] GameObject recolectVFX;
    private Collider parcelaCollider;
    public Collider barrierCollider;
    [SerializeField] GameObject openButton;
    [SerializeField] GameObject closeButton;

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
        else
        {
            DayNightSystem.Instance.dailyPowerUPPopUp();
        }
    }
    public void OpenSeedMenu()
    {
        ParcelaManager.instance.selectedParcela = gameObject.GetComponent<ParcelaOrder>();
        closeButton.SetActive(true);
        openButton.SetActive(false);
        PlayerStats.instance.blockMovement = true;
        ParcelaManager.instance.OpenSeedShop();
        if (TutorialManager.instance != null && TutorialManager.instance.currentStep == TutorialManager.Step.Interact)
        {
                TutorialManager.instance.CompleteStep();
        }
    }
    public void CloseSeedMenu()
    {
        ParcelaManager.instance.CloseSeedShop();
        closeButton.SetActive(false);
        openButton.SetActive(true);
        PlayerStats.instance.blockMovement = false;
        ParcelaManager.instance.selectedParcela = null;
        if (TutorialManager.instance != null && TutorialManager.instance.currentStep == TutorialManager.Step.ExitPlanting)
        {

                TutorialManager.instance.CompleteStep();
            
        }
    }
}
