using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public int money = 15;
    [SerializeField] TMP_Text moneyText;
    public static MoneySystem instance;
    public int totalMoneyEarned;
    [SerializeField] private Animator moneyAnimator;


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
       // moneyAnimator.SetTrigger("MoreMoney");
    }
    public void BuyMoney(int moneyToBuy)
    {
         money -= moneyToBuy;
         UpdateMoneyText();
      //  moneyAnimator.SetTrigger("MoreMoney");
    }
}
