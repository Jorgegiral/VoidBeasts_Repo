using UnityEngine;

public class RecolectVFX : MonoBehaviour
{
    private void Start()
    {
        DayNightSystem.Instance.ChangePopUPValue();
    }
    public void AddMoney()
    {
        MoneySystem.instance.AddMoney(ParcelaManager.instance.moneyToAdd);
        ParcelaManager.instance.moneyToAdd = 0;
    }
    public void StartDailyPowerUp()
    {
        DayNightSystem.Instance.dailyPowerUPPopUp();
        gameObject.SetActive(false);
    }
}
