using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public GameObject skillShop;

    public GameObject leftOneUI;
    public GameObject leftTwoUI;
    public GameObject midTwoUI;
    public GameObject rightOneUI;
    public GameObject rightTwoUI;

    [Header("Skill References")]
    public GameObject lockLeftOne;
    public GameObject lockRightOne;
    public GameObject lockMidTwo;
    public GameObject lockLeftTwoBlock;
    public GameObject lockLeftTwo;
    public GameObject lockRightTwoBlock;
    public GameObject lockRightTwo;
    public TMP_Text availablePointsText;

    public bool isRightOneUnlocked;
    public bool isMidTwoUnlocked;
    public bool isLeftOneUnlocked;
    public bool isLeftTwoUnlocked;
    public bool isRightTwoUnlocked;
    public bool freeUpgrade;
    private int availablePoints = 3;
    public int maxPoints = 2;
    private bool skillShopOpened;
    private string key;
    private string localizedString;
    private void Awake()
    {
        if (instance == null) { instance = this; }

        isLeftOneUnlocked = false;
        isRightOneUnlocked = false;
        isMidTwoUnlocked = false;
        isLeftTwoUnlocked = false;
        isRightTwoUnlocked = false;
        key = "Points";
        localizedString = LocalizationSettings.StringDatabase.GetLocalizedString("Tabla1", key);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && DayNightSystem.Instance.isDay)
        {
            if (!skillShopOpened)
            {
                OpenSkillsShop();
            }
            else
            {
                CloseSkillsShop();
            }
        }
    }
    private void UpdateSkillValues()
    {
        availablePointsText.text = localizedString + availablePoints.ToString();
    }

    public void UnlockRightOne(int precio)
    {
        if (freeUpgrade)
        {
            isRightOneUnlocked = true;
            rightOneUI.SetActive(true);
            lockRightOne.SetActive(false);
            lockRightTwoBlock.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            freeUpgrade = false;
        }
        else
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.BuyMoney(precio);
            isRightOneUnlocked = true;
            rightOneUI.SetActive(true);
            lockRightOne.SetActive(false);
            lockRightTwoBlock.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockRightTwo(int precio)
    {
        if (freeUpgrade)
        {
            isRightTwoUnlocked = true;
            rightTwoUI.SetActive(true);
            lockRightTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            freeUpgrade = false;
        }
        else
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.BuyMoney(precio);
            isRightTwoUnlocked = true;
            rightTwoUI.SetActive(true);
            lockRightTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockLeftOne(int precio)
    {
        if (freeUpgrade)
        {
            isLeftOneUnlocked = true;
            leftOneUI.SetActive(true);
            lockLeftOne.SetActive(false);
            lockLeftTwoBlock.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            freeUpgrade = false;
        }
        else
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.BuyMoney(precio);
            isLeftOneUnlocked = true;
            leftOneUI.SetActive(true);
            lockLeftOne.SetActive(false);
            lockLeftTwoBlock.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockLeftTwo(int precio)
    {
        if (freeUpgrade)
        {
            isLeftTwoUnlocked = true;
            leftTwoUI.SetActive(true);
            lockLeftTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            freeUpgrade = false;
        }
        else
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.BuyMoney(precio);
            isLeftTwoUnlocked = true;
            leftTwoUI.SetActive(true);
            lockLeftTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockMidTwo(int precio)
    {
        if (freeUpgrade)
        {
            isMidTwoUnlocked = true;
            midTwoUI.SetActive(true);
            lockMidTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            freeUpgrade = false;
        }
        else
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.BuyMoney(precio);
            isMidTwoUnlocked = true;
            midTwoUI.SetActive(true);
            lockMidTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void OpenSkillsShop()
    {
        if (PlayerStats.instance.menuOpened) return;
        skillShopOpened = true;
        skillShop.gameObject.SetActive(true);
        UpdateSkillValues();
        PlayerStats.instance.menuOpened = true;
    }
    public void CloseSkillsShop()
    {
        skillShop.gameObject.SetActive(false);
        skillShopOpened = false;
        PlayerStats.instance.menuOpened = false;
    }
    public void AddPoint()
    {
        availablePoints++;  
    }
}
