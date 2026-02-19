using UnityEngine;

public class RecolectVFX : MonoBehaviour
{
    public void AddMoney()
    {
        MoneySystem.instance.AddMoney(ParcelaManager.instance.moneyToAdd);
        ParcelaManager.instance.moneyToAdd = 0;
    }
}
