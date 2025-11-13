using UnityEngine;

public class ParcelaManager : MonoBehaviour
{

    public static ParcelaManager instance;
    public ParcelaOrder selectedParcela;
    public bool freeSeed = true;
    public GameObject freeSeedText;
    private bool isParcelaFull;
    private void Awake()
    {
        if (instance == null) { instance = this; }
       
    }

    public void AddPlant(Plants plantToBuy)
    {
        if (!freeSeed && !CheckFullParcela())
        {
            if (plantToBuy.precio > MoneySystem.instance.money)
                return;

            MoneySystem.instance.BuyMoney(plantToBuy.precio);

            for (int i = 0; i < selectedParcela.parcelas.Length; i++)
            {
                if (selectedParcela.parcelas[i].PlantIsFull())
                    continue;
                selectedParcela.parcelas[i].plant = plantToBuy;
                selectedParcela.parcelas[i].PlantIsFull();
                selectedParcela.parcelas[i].Planted();
                break;
            }
        }
        if (freeSeed && !CheckFullParcela())
        {
            for (int i = 0; i < selectedParcela.parcelas.Length; i++)
            {
                if (selectedParcela.parcelas[i].PlantIsFull())
                    continue;
                selectedParcela.parcelas[i].plant = plantToBuy;
                selectedParcela.parcelas[i].PlantIsFull();
                selectedParcela.parcelas[i].Planted();
                if (freeSeed)
                {
                    freeSeed = false;
                    freeSeedText.SetActive(false);
                }
                break;

            }
        }
        bool CheckFullParcela()
        {
            foreach (var parcela in selectedParcela.parcelas)
            {
                if (!parcela.PlantIsFull())
                    return false;
            }
            return true;
        }
    }
}




