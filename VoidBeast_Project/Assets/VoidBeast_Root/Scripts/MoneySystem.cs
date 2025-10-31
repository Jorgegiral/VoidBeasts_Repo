using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    int money;
    [SerializeField] TMP_Text moneyText;


    // Update is called once per frame
    void Update()
    {
        UpdateMoneyText();
    }
    void UpdateMoneyText()
    {
        if (DayNightSystem.Instance.isDay)
        {
            moneyText.text = money.ToString();
        }
    }
    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }
    public void BuyMoney(int moneyToBuy)
    {
        if(money - moneyToBuy < 0)
        {

        }
        else
        {
            money -= moneyToBuy;
        }
        
    }
}
