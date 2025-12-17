using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("UI References")]
    public GameObject upgradeShop;


    [Header("Stats References")]
    public int mainBuildingLevel = 1;
    public int wallLevel = 0;
    public int towerLevel = 0;
    public int wallAvailable = 25;
    public int towerAvailable = 2;
    public int cropAvailable = 2;
    public TypeBuild wallBuild;
    public TypeBuild towerBuild;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        wallBuild.build = wallBuild.levelModels[wallLevel];
        towerBuild.build = towerBuild.levelModels[towerLevel];


    }

    public void UpgradeWall(int precio)
    {
        if(precio <= MoneySystem.instance.money && wallLevel != 2)
        {
            wallLevel++;
            wallBuild.Upgrade(wallLevel);

        }
    }
    public void UpgradeTower(int precio)
    {
        if (precio <= MoneySystem.instance.money && towerLevel != 2)
        {
            towerLevel++;
            towerBuild.Upgrade(towerLevel);
            Debug.Log("Upgraded");
        }
    }
    public void UpgradeMainBuild(int precio)
    {
        if (precio <= MoneySystem.instance.money && mainBuildingLevel != 6)
        {
            mainBuildingLevel++;
        }
    }
    public void OpenUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(true);
    }
    public void CloseUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(false);
    }
}
