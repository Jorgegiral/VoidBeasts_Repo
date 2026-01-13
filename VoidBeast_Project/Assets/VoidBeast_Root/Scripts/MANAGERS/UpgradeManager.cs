using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("UI References")]
    public GameObject upgradeShop;


    [Header("Stats References")]
    public int mainBuildingLevel = 1;
    public int wallLevel = 0;
    public int towerLevel = 0;
    public int wallAvailable = 10;
    public int towerAvailable = 0;
    public int cropAvailable = 1;
    public TypeBuild wallBuild;
    public TypeBuild towerBuild;
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();
    public GameObject[] mainBuildModels;
    private int maxTowerLevel = 3;
    private int maxWallLevel = 3;



    private void Awake()
    {
        if (instance == null) { instance = this; }
        wallBuild.build = wallBuild.levelModels[wallLevel];
        towerBuild.build = towerBuild.levelModels[towerLevel];


    }

    public void UpgradeWall(int precio)
    {
        if (wallLevel >= maxWallLevel) return;
        if (precio > MoneySystem.instance.money) return;
            MoneySystem.instance.money -= precio;
            wallLevel++;
            wallBuild.Upgrade(wallLevel);
            if (walls.Count > 0) SwapWallModelsOnUpgrade();

    }
    public void UpgradeTower(int precio)
    {
        if (towerLevel >= maxTowerLevel) return;
        if (precio > MoneySystem.instance.money) return;
            MoneySystem.instance.money -= precio;
            towerLevel++;
            towerBuild.Upgrade(towerLevel);
            if(towers.Count > 0) SwapTowerModelsOnUpgrade();

        
    }
    public void UpgradeMainBuild(int precio)
    {
        if (precio <= MoneySystem.instance.money && mainBuildingLevel != 6)
        {
            mainBuildingLevel++;
        }
        if(mainBuildingLevel == 2)
        {
            wallAvailable += 5;
            cropAvailable += 1;
        }
        if (mainBuildingLevel == 3)
        {
            towerAvailable += 1;
        }
        if (mainBuildingLevel == 4)
        {
            wallAvailable += 5;
        }
        if (mainBuildingLevel == 5)
        {
            cropAvailable += 1;
            wallAvailable += 5;
        }
        if (mainBuildingLevel == 6)
        {
            wallAvailable += 10;
            towerAvailable += 1;
        }
    }
    public void SwapWallModelsOnUpgrade()
    {
        
        for (int i = walls.Count - 1; i >= 0; i++)
        {
            Vector3 position = walls[i].transform.position;
            Quaternion rotation = walls[i].transform.rotation;
            Instantiate(wallBuild.build, position, rotation);
            WallHP wallHP = walls[i].GetComponent<WallHP>();
            wallHP.TakeDamage(100000);

        }

    }
    public void SwapTowerModelsOnUpgrade()
    {

        for (int i = towers.Count-1; i >= 0; i--)
        {
            Vector3 position = towers[i].transform.position;
            Quaternion rotation = towers[i].transform.rotation;
            Instantiate(towerBuild.build, position, rotation);
            TowerHP towerHP = towers[i].GetComponent<TowerHP>();
            towerHP.TakeDamage(100000);
            
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
