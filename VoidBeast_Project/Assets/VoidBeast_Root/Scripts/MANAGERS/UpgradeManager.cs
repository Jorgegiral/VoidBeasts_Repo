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
    private int maxTowerLevel = 3;
    private int maxWallLevel = 3;
    public bool isGunUnlocked;
    public bool isSpinUnlocked;
    public bool isMineUnlocked;
    public bool isBombUnlocked;
    public bool isRayUnlocked;
    public BoundsInt bounds;
    public GameObject[] updateModelMainBuild;

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

public void UpgradeWall(int precio)
    {
        if (wallLevel >= maxWallLevel) return;
        if (precio > MoneySystem.instance.money) return;
        if (walls.Count <= 0) return;
            MoneySystem.instance.money -= precio;
            wallLevel++;
            wallBuild.Upgrade(wallLevel);
            SwapWallModelsOnUpgrade();

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
        if (precio <= MoneySystem.instance.money )
        {
            MoneySystem.instance.money -= precio;
            mainBuildingLevel++;
            switch (mainBuildingLevel)
            {
                case 2:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[1].SetActive(true);
                    wallAvailable += 5;
                    cropAvailable += 1;
                    break;
                case 3:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[2].SetActive(true);
                    towerAvailable += 1;
                    break;
                case 4:
                    ExpandBuildArea(4, 4);
                    updateModelMainBuild[3].SetActive(true);
                    updateModelMainBuild[1].SetActive(false);
                    wallAvailable += 5;
                    break;
                case 5:
                    ExpandBuildArea(2, 2);
                    updateModelMainBuild[4].SetActive(true);
                    cropAvailable += 1;
                    wallAvailable += 5;
                    break;
                case 6:
                    ExpandBuildArea(4, 4);
                    updateModelMainBuild[5].SetActive(true);
                    updateModelMainBuild[4].SetActive(false);

                    wallAvailable += 10;
                    towerAvailable += 1;
                    break;
                default: break;
            }
        }
       
    }
    public void UnlockGun(int precio)
    {
        if (precio <= MoneySystem.instance.money)
        {
            MoneySystem.instance.money -= precio;
            isGunUnlocked = true;
        }
    }
    public void UnlockRay(int precio)
    {
        if (precio <= MoneySystem.instance.money)
        {
            MoneySystem.instance.money -= precio;
            isRayUnlocked = true;
            RayUI.SetActive(true);

        }
    }
    public void UnlockMine(int precio)
    {
        if (precio <= MoneySystem.instance.money)
        {
            MoneySystem.instance.money -= precio;
            isMineUnlocked = true;
            MineUI.SetActive(true);

        }
    }
    public void UnlockBomb(int precio)
    {
        if (precio <= MoneySystem.instance.money)
        {
            MoneySystem.instance.money -= precio;
            isBombUnlocked = true;
            bombUI.SetActive(true);
        }
    }
    public void UnlockSpin(int precio)
    {
        if (precio <= MoneySystem.instance.money)
        {
            MoneySystem.instance.money -= precio;
            isSpinUnlocked = true;
            SpinUI.SetActive(true);

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
            wallHP.TakeDamage(100000);
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
            towerHP.TakeDamage(100000);
            
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



    public void OpenUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(true);
    }
    public void CloseUpgradeShop()
    {
        upgradeShop.gameObject.SetActive(false);
    }
    public void OpenSkillsShop()
    {
        skillShop.gameObject.SetActive(true);
    }
    public void CloseSkillsShop()
    {
        skillShop.gameObject.SetActive(false);
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
