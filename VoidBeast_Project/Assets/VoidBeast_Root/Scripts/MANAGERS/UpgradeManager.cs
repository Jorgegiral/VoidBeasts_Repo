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
    public TMP_Text upgradesText;

    public GameObject buildBlock;
    public GameObject towerBlock;
    public GameObject wallBlock;
    [SerializeField] GameObject buyDailyPowerUp;

    private int availablePoints = 3;
    public TMP_Text availablePointsText;
    [SerializeField] GameObject upgradeVFX;
    [SerializeField] Transform mbuild;
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
    public BoundsInt bounds;
    public GameObject[] updateModelMainBuild;
    private int mainBuildvalue = 250;
    private int wallBuildvalue = 500;
    private int towerBuildvalue = 500;
    public int upgradeValue = 100;
    private bool upgradeShopOpened;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        wallBuild.build = wallBuild.levelModels[wallLevel];
        towerBuild.build = towerBuild.levelModels[towerLevel];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && DayNightSystem.Instance.isDay)
        {
            if (!upgradeShopOpened)
            {
                OpenUpgradeShop();
            }
            else
            {
                CloseUpgradeShop();
            }
        }
    }
    public void UpgradeWall()
    {
        if (wallLevel >= maxWallLevel) return;
        if (wallBuildvalue > MoneySystem.instance.money) return;
            MoneySystem.instance.BuyMoney(wallBuildvalue);
            wallLevel++;
            wallBuild.Upgrade(wallLevel);
            wallBuildvalue += 500;
            UpdateValuesBuildings();
            MoneySystem.instance.UpdateMoneyText();
         if(walls.Count>0)   SwapWallModelsOnUpgrade();
        if (wallLevel == maxWallLevel) wallBlock.SetActive(true);

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
        if(towerLevel == maxTowerLevel) towerBlock.SetActive(true);

        
    }
    public void UpgradeMainBuild()
    {
        if (mainBuildingLevel == 6) return;
        if (mainBuildvalue > MoneySystem.instance.money ) return;
        else { 
        
            MoneySystem.instance.BuyMoney(mainBuildvalue);
            mainBuildingLevel++;
            GameObject upgradeVFXobj = Instantiate(upgradeVFX, mbuild);
            Destroy(upgradeVFXobj,3f);
            switch (mainBuildingLevel)
            {
                case 2:
                    ExpandBuildArea(1, 1);
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
                    ExpandBuildArea(1, 1);
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
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[5].SetActive(true);
                    updateModelMainBuild[4].SetActive(false);
                    wallAvailable += 18;
                    maxWall += 18;
                    towerAvailable += 2;
                    maxTower += 2;
                    cropAvailable += 1;
                    maxCrop += 1;
                    buildBlock.SetActive(true);
                    UpdateValuesBuildings();
                    MoneySystem.instance.UpdateMoneyText();
                    buyDailyPowerUp.SetActive(true);
                    break;
                default: break;
            }
        }
       
    }
   
    public void SwapWallModelsOnUpgrade()
    {

        List<GameObject> newWalls = new List<GameObject>();

        foreach (var wall in walls.ToList())
        {
            if (wall == null) continue;
            UnRegisterWall(wall);
            Vector3 position = wall.transform.position;
            Quaternion rotation = wall.transform.rotation;
            GameObject newWall = Instantiate(wallBuild.build, position, rotation);
            Building oldWallBuild = wall.GetComponent<Building>();
            Building newWallBuild = newWall.GetComponent<Building>();
            newWallBuild.area.position = oldWallBuild.area.position;
            newWalls.Add(newWall);
            WallHP wallHP = wall.GetComponent<WallHP>();
            if (wallHP == null) continue;
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
    public void TakeUpgrade()
    {
        availablePoints++;
    }
    private void UpdateValuesBuildings()
    {
        mainBuildText.text = mainBuildvalue.ToString();
        towerText.text = towerBuildvalue.ToString();
        wallText.text = wallBuildvalue.ToString();
    }
    public void UpdateValuePowerUp()
    {
        upgradesText.text = upgradeValue.ToString();
    }

    public void OpenUpgradeShop()
    {
        if (PlayerStats.instance.menuOpened) return;
        upgradeShop.gameObject.SetActive(true);
        UpdateValuesBuildings();
        PlayerStats.instance.menuOpened = true;
        upgradeShopOpened = true;

    }
    public void CloseUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(false);
        PlayerStats.instance.menuOpened = false;
        upgradeShopOpened = false;

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
