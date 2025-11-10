using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int money;
    [SerializeField] TMP_Text moneyText;
    public static MoneySystem instance;
    public int totalMoneyEarned;

    void Awake()
    {
        if (instance == null) { instance = this; }

    }
    private void Start()
    {
        UpdateMoneyText();
    }
    public void UpdateMoneyText()
    {
        if (DayNightSystem.Instance.isDay)
        {
            moneyText.text = money.ToString();
        }
    }
    public void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        UpdateMoneyText();
        totalMoneyEarned += moneyToAdd;
    }
    public void BuyMoney(int moneyToBuy)
    {
         money -= moneyToBuy;
         UpdateMoneyText();
    }
}
