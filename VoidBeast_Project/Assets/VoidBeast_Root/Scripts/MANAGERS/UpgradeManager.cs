using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Linq;
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
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();



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
            if (walls.Count > 0) SwapWallModelsOnUpgrade();

        }
    }
    public void UpgradeTower(int precio)
    {
        if (precio <= MoneySystem.instance.money && towerLevel != 2)
        {
            towerLevel++;
            towerBuild.Upgrade(towerLevel);
            if(towers.Count > 0) SwapTowerModelsOnUpgrade();

        }
    }
    public void UpgradeMainBuild(int precio)
    {
        if (precio <= MoneySystem.instance.money && mainBuildingLevel != 6)
        {
            mainBuildingLevel++;
        }
    }
    public void SwapWallModelsOnUpgrade()
    {
        
        for (int i = walls.Count - 1; i >= 0; i++)
        {
            Instantiate(wallBuild.build, walls[i].transform);
            UnRegisterWall(walls[i]);
            Destroy(walls[i]);

        }

    }
    public void SwapTowerModelsOnUpgrade()
    {
        for (int i = towers.Count - 2; i >= 0; i++)
        {
            Instantiate(towerBuild.build, towers[0].transform);
            UnRegisterTower(towers[0]);
            Destroy(towers[0]);
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
    public void RegisterWall(GameObject wall)
    {
        walls.Add(wall);

    }
    public void UnRegisterWall(GameObject wall)
    {
        walls.Remove(wall);
    }
    public void RegisterTower(GameObject tower)
    {
        towers.Add(tower);
    }
    public void UnRegisterTower(GameObject tower)
    {
        towers.Remove(tower);

    }
}
