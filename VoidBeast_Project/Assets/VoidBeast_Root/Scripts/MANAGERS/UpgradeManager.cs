using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("UI References")]
    public GameObject upgradeShop;
    public GameObject skillShop;
    public TMP_Text mainBuildText;
    public TMP_Text wallText;
    public TMP_Text towerText;

    public GameObject bombUI;
    public GameObject RayUI;
    public GameObject MineUI;
    public GameObject SpinUI;

    [Header("Skill References")]
    public GameObject lockMine;
    public GameObject lockGun;
    public GameObject lockSpin;
    public GameObject lockBombTwo;
    public GameObject lockBomb;
    public GameObject lockRayTwo;
    public GameObject lockRay;
    private int availablePoints = 3;
    public TMP_Text availablePointsText;


    [Header("Stats References")]
    public int mainBuildingLevel = 1;
    public int wallLevel = 0;
    public int towerLevel = 0;
    public int wallAvailable = 8;
    public int towerAvailable = 0;
    public int cropAvailable = 1;
    public int cropsBought = 0;
    public int towerBought = 0;
    public int wallBought = 0;
    public int maxTower = 0;
    public int maxWall = 8;
    public int maxCrop = 1;
    public TypeBuild wallBuild;
    public TypeBuild towerBuild;
    public List<GameObject> walls = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();
    private int maxTowerLevel = 2;
    private int maxWallLevel = 2;
    public bool isGunUnlocked;
    public bool isSpinUnlocked;
    public bool isMineUnlocked;
    public bool isBombUnlocked;
    public bool isRayUnlocked;
    public BoundsInt bounds;
    public GameObject[] updateModelMainBuild;
    private int mainBuildvalue = 250;
    private int wallBuildvalue = 500;
    private int towerBuildvalue = 500;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        wallBuild.build = wallBuild.levelModels[wallLevel];
        towerBuild.build = towerBuild.levelModels[towerLevel];
        isGunUnlocked = false;
        isSpinUnlocked = false;
        isMineUnlocked = false;
        isBombUnlocked = false;
        isRayUnlocked = false;
}

public void UpgradeWall()
    {
        if (wallLevel >= maxWallLevel) return;
        if (wallBuildvalue > MoneySystem.instance.money) return;
        if (walls.Count <= 0) return;
            MoneySystem.instance.BuyMoney(wallBuildvalue);
            wallLevel++;
            wallBuild.Upgrade(wallLevel);
            wallBuildvalue += 500;
            UpdateValuesBuildings();
        MoneySystem.instance.UpdateMoneyText();
            SwapWallModelsOnUpgrade();

    }
    public void UpgradeTower()
    {
        if (towerLevel >= maxTowerLevel) return;
        if (towerBuildvalue > MoneySystem.instance.money) return;
            MoneySystem.instance.BuyMoney(towerBuildvalue);
            towerLevel++;
            towerBuild.Upgrade(towerLevel);
            towerBuildvalue += 500;
        UpdateValuesBuildings();
        MoneySystem.instance.UpdateMoneyText();
        if (towers.Count > 0) SwapTowerModelsOnUpgrade();

        
    }
    public void UpgradeMainBuild()
    {
        if (mainBuildvalue > MoneySystem.instance.money ) return;
        else { 
        
            MoneySystem.instance.BuyMoney(mainBuildvalue);
            mainBuildingLevel++;
            switch (mainBuildingLevel)
            {
                case 2:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[1].SetActive(true);
                    wallAvailable += 5;
                    maxWall += 5;
                    cropAvailable += 1;
                    maxCrop += 1;
                    towerAvailable += 1;
                    maxTower += 1;
                    mainBuildvalue += 250;
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    break;
                case 3:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[2].SetActive(true);
                    mainBuildvalue += 400;
                    wallAvailable += 2;
                    maxWall += 2;
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    break;
                case 4:
                    ExpandBuildArea(4, 4);
                    updateModelMainBuild[3].SetActive(true);
                    updateModelMainBuild[1].SetActive(false);
                    wallAvailable += 5;
                    maxWall += 5;
                    towerAvailable += 1;
                    maxTower += 1;
                    mainBuildvalue += 500;
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    break;
                case 5:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[4].SetActive(true);
                    cropAvailable += 1;
                    maxCrop += 1;
                    wallAvailable += 5;
                    maxWall += 5;
                    mainBuildvalue += 500;
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    break;
                case 6:
                    ExpandBuildArea(4, 4);
                    updateModelMainBuild[5].SetActive(true);
                    updateModelMainBuild[4].SetActive(false);
                    wallAvailable += 10;
                    maxWall += 10;
                    towerAvailable += 1;
                    maxTower += 1;
                    mainBuildvalue += 1000;
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    break;
                default: break;
            }
        }
       
    }
    public void UnlockGun(int precio)
    {
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.money -= precio;
            isGunUnlocked = true;
            lockGun.SetActive(false);
            lockRayTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockRay(int precio)
    {
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.money -= precio;
            isRayUnlocked = true;
            RayUI.SetActive(true);
            lockRay.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockMine(int precio)
    {
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.money -= precio;
            isMineUnlocked = true;
            MineUI.SetActive(true);
            lockMine.SetActive(false);
            lockBombTwo.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockBomb(int precio)
    {
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.money -= precio;
            isBombUnlocked = true;
            bombUI.SetActive(true);
            lockBomb.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void UnlockSpin(int precio)
    {
        if (precio <= MoneySystem.instance.money && availablePoints > 0)
        {
            MoneySystem.instance.money -= precio;
            isSpinUnlocked = true;
            SpinUI.SetActive(true);
            lockSpin.SetActive(false);
            availablePoints--;
            UpdateSkillValues();
            MoneySystem.instance.UpdateMoneyText();
        }
    }
    public void SwapWallModelsOnUpgrade()
    {

        List<GameObject> newWalls = new List<GameObject>();

        foreach (var wall in walls.ToList())
        {
            Vector3 position = wall.transform.position;
            Quaternion rotation = wall.transform.rotation;
            GameObject newWall = Instantiate(wallBuild.build, position, rotation);
            newWalls.Add(newWall);
            WallHP wallHP = wall.GetComponent<WallHP>();
            wallHP.UpgradeDamage(100000);
        }
        walls = newWalls;
        AdaptModels();
    }
    public void AdaptModels()
    {
        foreach (var wall in walls)
        {
            WallBehaviour refresh = wall.GetComponent<WallBehaviour>();
            refresh.ThrowRaycastNeighbours();
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
            towerHP.UpgradeDamage(100000);
            
        }
    }
    public void ExpandBuildArea(int extraWidth, int extraHeight)
    {
        bounds.xMin -= extraWidth / 2;
        bounds.xMax += extraWidth / 2;
        bounds.yMax += extraHeight /2; 
        bounds.yMin -= extraHeight / 2;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (GridBuilding.instance.mainTilemap.GetTile(pos) == null) 
                {
                    GridBuilding.instance.mainTilemap.SetTile(pos, GridBuilding.instance.GetTileColor(GridBuilding.TileType.White));
                }
        }
        }
    }

    private void UpdateValuesBuildings()
    {
        mainBuildText.text = mainBuildvalue.ToString();
        towerText.text = towerBuildvalue.ToString();
        wallText.text = wallBuildvalue.ToString();
    }
    private void UpdateSkillValues()
    {
        availablePointsText.text = "POINTS: " + availablePoints.ToString();
    }

    public void OpenUpgradeShop()
    {
        if (PlayerStats.instance.menuOpened) return;
        upgradeShop.gameObject.SetActive(true);
        UpdateValuesBuildings();
        PlayerStats.instance.menuOpened = true;
    }
    public void CloseUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(false);
        PlayerStats.instance.menuOpened = false;

    }
    public void OpenSkillsShop()
    {
        if (PlayerStats.instance.menuOpened) return;
        skillShop.gameObject.SetActive(true);
        UpdateSkillValues();
        PlayerStats.instance.menuOpened = true;
    }
    public void CloseSkillsShop()
    {
        skillShop.gameObject.SetActive(false);
        PlayerStats.instance.menuOpened = false;
    }
    public void RegisterWall(GameObject wall)
    {
        if (!walls.Contains(wall))
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
