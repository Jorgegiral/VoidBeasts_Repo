using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class GridBuilding : MonoBehaviour
{
    public static GridBuilding instance;
    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();
    public Building buildingTemp;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    [SerializeField] LayerMask layerGround;
    private Vector3 buildingOffset;
    private Vector3 buildingClickOffset;
    public bool isWall;
    public bool isBuild;
    public bool isTower;

    bool parcela = false; //Jorge
    private TypeBuild currentBuildType;
    private bool positionLocked = false;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    private void Update()
    {
        if (buildingTemp != null && !buildingTemp.Placed && !positionLocked)
        {
            FollowMouse();
            FollowBuilding(); 
        }
    }

    private void Start()
    {
        string tilepath = @"Tiles/";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilepath + "white"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilepath + "red"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilepath + "green"));


    }
    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }
    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }
    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }
    public TileBase GetTileColor(TileType type)
    {
        TileBase tileBaseColor = tileBases[type];
        return tileBaseColor;
    }
    public void InitializeWithBuilding(TypeBuild build)
    {
        if (buildingTemp != null) return;
        if (!CheckIfAvailable(build)) return;
        if (build.precio > MoneySystem.instance.money) return;
        currentBuildType = build; // Jorge
        buildingTemp = Instantiate(build.build, Vector3.zero + new Vector3(0,0,-5), Quaternion.identity).GetComponent<Building>();
        buildingOffset = build.placeOffSet;
        buildingClickOffset = build.clickOffSet;
        MoneySystem.instance.BuyMoney(build.precio);
       // FollowBuilding();
    }
    private bool CheckIfAvailable(TypeBuild build)
    {
        switch (build.type.ToString())
        {
            case ("Wall"):
                if (UpgradeManager.instance.wallAvailable > 0)
                {
                    UpgradeManager.instance.wallAvailable--;
                    isWall = true;
                    isTower = false;
                    isBuild = false;
                    return true;
                }
                return false;

            case ("Tower"):
                if (UpgradeManager.instance.towerAvailable > 0)
                {
                    UpgradeManager.instance.towerAvailable--;
                    isTower = true;
                    isBuild = false;
                    isWall = false;
                    return true;
                }
                return false;

            case ("Build"):
                if (UpgradeManager.instance.cropAvailable > 0)
                {
                    UpgradeManager.instance.cropAvailable--;
                    isBuild = true;
                    isWall = false;
                    isTower =false;
                    return true;
                }
                return false;
        }

        return false;
    }
    private void CancelOption()
    {
        if (isBuild)
        {
            UpgradeManager.instance.cropAvailable++;
            MoneySystem.instance.AddMoney(15);
        }
        if (isWall)
        {
            UpgradeManager.instance.wallAvailable++;
            MoneySystem.instance.AddMoney(5);
        }
        if (isTower)
        {
            UpgradeManager.instance.towerAvailable++;
            MoneySystem.instance.AddMoney(100);
        }
    }
    private void FollowBuilding()
    {
        ClearArea();
        buildingTemp.area.position = gridLayout.WorldToCell(buildingTemp.gameObject.transform.position + buildingOffset);
        BoundsInt buildingArea = buildingTemp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);
        int size = baseArray.Length;
        TileBase[] tilearray = new TileBase[size];
        for(int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tilearray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tilearray,TileType.Red);
                break;
            }
        }
        tempTilemap.SetTilesBlock(buildingArea,tilearray);
        prevArea = buildingArea;
    }
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }
    public bool CanTakeAre(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);
        foreach(var col in baseArray)
        {
            if (col != tileBases[TileType.White]) 
            {
                Debug.Log("no se puede construir");
                return false;
            }

        }
        return true;
    }
    public void TakeArea(BoundsInt area)
    {
        if (isWall)
        {
            WallBehaviour wall = buildingTemp.GetComponent<WallBehaviour>();
            wall.ThrowRaycast();
        }
        SetTilesBlock(area, TileType.Empty,tempTilemap);
        SetTilesBlock(area,TileType.Red,mainTilemap);
        buildingTemp = null;
        isWall = false;
    }
    public void UnTakeArea(BoundsInt area)
    {
        if (isWall)
        {
            WallBehaviour wall = buildingTemp.GetComponent<WallBehaviour>();
            wall.ThrowRaycast();
        }
        SetTilesBlock(area, TileType.Empty, tempTilemap);
        SetTilesBlock(area, TileType.White, mainTilemap);
        buildingTemp = null;
        isWall = false;
    }
    private void FollowMouse()
    {
        if (EventSystem.current != null &&
    EventSystem.current.IsPointerOverGameObject())
            return;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerGround))
        {
            Vector3Int cellPos = gridLayout.WorldToCell(hit.point);

            if (prevPos != (Vector3)cellPos)
            {
                buildingTemp.transform.localPosition =
                    gridLayout.CellToLocalInterpolated(cellPos + buildingClickOffset);

                prevPos = cellPos;
            }
        }
    }
    public void LockPositionBuildAction(InputAction.CallbackContext context)
    {
        if (!buildingTemp) return;
        if (buildingTemp.Placed) return;
        Debug.Log("asda");
        if (buildingTemp.CanBePlaced())
        {
            positionLocked = true;

        }
        BuildConfirmUI.instance.Show();
        FollowBuilding();
    }
    public void UnLockPositionBuildAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!buildingTemp) return;
        if (buildingTemp.Placed) return;

        if (positionLocked)
        {
            positionLocked = false;

        }
        BuildConfirmUI.instance.Hide();
        FollowBuilding();
    }

    public void BuildAction()
    {

        if (buildingTemp.CanBePlaced())
        {
            buildingTemp.Place();
            positionLocked = false;
            BuildConfirmUI.instance.Hide();

            //Jorge:

            if (TutorialManager.instance != null)
            {

                if (!parcela)
                {
                    if (TutorialManager.instance.step == 3 &&
                        currentBuildType.type == TypeBuild.BuildType.Build)
                    {
                        TutorialManager.instance.CompleteStep();
                    }
                }

                currentBuildType = null;
            }
        }
    }
    public void CancelBuildAction()
    {
        if (!buildingTemp) return;
        Building temp = buildingTemp;
        buildingTemp = null;
        positionLocked = false;
        ClearArea();
        BuildConfirmUI.instance.Hide();
        CancelOption();
        Destroy(temp.gameObject);

    }


    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }

}
