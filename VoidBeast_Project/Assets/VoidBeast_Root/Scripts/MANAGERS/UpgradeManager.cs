using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public int mainBuildingLevel = 1;
    public int wallLevel = 1;
    public int towerLevel = 1;
    public int wallAvailable = 25;
    public int towerAvailable = 2;
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void UpgradeWall(int precio)
    {
        if(precio < MoneySystem.instance.money && wallLevel != 3)
        {
            wallLevel++;

        }
    }
    public void UpgradeTower(int precio)
    {
        if (precio < MoneySystem.instance.money && towerLevel != 3)
        {
            towerLevel++;
        }
    }
    public void UpgradeMainBuild(int precio)
    {
        if (precio < MoneySystem.instance.money && towerLevel != 6)
        {
            mainBuildingLevel++;
        }
    }
}
