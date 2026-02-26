using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MenuManager : MonoBehaviour
{
    [Header("Skill Menu")]
    public GameObject skillShop;
    public TMP_Text availablePointsText;
    string localizedString;
    private string key;

    [Header("Pause Menu")]
    [SerializeField] GameObject escapeMenu;
    [SerializeField] GameObject optionsPanel;

    [Header("Upgrade Menu")]
    public GameObject upgradeShop;
    private bool upgradeShopOpened;
    public TMP_Text mainBuildText;
    public TMP_Text wallText;
    public TMP_Text towerText;
    private bool shopOpened;

    private GameObject currentMenu;
    private bool openMenu;
    private void Awake()
    {
        key = "Points";
        localizedString = LocalizationSettings.StringDatabase.GetLocalizedString("Tabla1", key);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!shopOpened && DayNightSystem.Instance.isDay)
            {
                if(currentMenu != null && openMenu)
                {
                    currentMenu.SetActive(false);
                    openMenu = false;
                    upgradeShopOpened = false;
                }
                OpenSkillsShop();
                currentMenu = skillShop;
            }
            else
            {
                CloseSkillsShop();
                currentMenu = null;

            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!upgradeShopOpened && DayNightSystem.Instance.isDay)
            {
                if (currentMenu != null && openMenu)
                {
                    currentMenu.SetActive(false);
                    openMenu = false;
                    shopOpened = false;
                }
                OpenUpgradeShop();
                currentMenu = upgradeShop;
            }
            else
            {
                CloseUpgradeShop();
                currentMenu = null;
            }
        }
    }
    public void OpenSkillsShop()
    {
        if(openMenu) return;
        shopOpened = true;
        skillShop.gameObject.SetActive(true);
        UpdateSkillValues();
        PlayerStats.instance.menuOpened = true;
        openMenu = true;
    }
    public void CloseSkillsShop()
    {
        skillShop.gameObject.SetActive(false);
        shopOpened = false;
        PlayerStats.instance.menuOpened = false;
        openMenu=false;
    }
    public void UpdateSkillValues()
    {
        availablePointsText.text = localizedString + SkillManager.instance.availablePoints.ToString();
    }
    public void OpenUpgradeShop()
    {

        if (openMenu) return;
        upgradeShop.gameObject.SetActive(true);
        UpdateValuesBuildings();
        PlayerStats.instance.menuOpened = true;
        openMenu = true;
        upgradeShopOpened = true;

    }
    public void CloseUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(false);
        PlayerStats.instance.menuOpened = false;
        openMenu = false;
        upgradeShopOpened = false;

    }
    private void UpdateValuesBuildings()
    {
        mainBuildText.text = UpgradeManager.instance.mainBuildvalue.ToString();
        towerText.text = UpgradeManager.instance.towerBuildvalue.ToString();
        wallText.text = UpgradeManager.instance.wallBuildvalue.ToString();
    }
    public void OnEscapeButton(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (!PlayerStats.instance.escapeMenuOpened)
        {
            escapeMenu.SetActive(true);
            PlayerStats.instance.menuOpened = true;

            PlayerStats.instance.escapeMenuOpened = true;
            Time.timeScale = 0f;
            Settings.instance.StopSingleSoundFX();
        }
        else
        {
            escapeMenu.SetActive(false);
            optionsPanel.SetActive(false);
            PlayerStats.instance.menuOpened = false;
            PlayerStats.instance.escapeMenuOpened = false;
            Time.timeScale = 1f;
        }
    }
}
