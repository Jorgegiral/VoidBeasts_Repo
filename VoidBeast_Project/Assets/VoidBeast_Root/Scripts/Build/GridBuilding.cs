using System.Collections.Generic;
using UnityEngine;
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
    private Building buildingTemp;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    [SerializeField] LayerMask layerGround;
    private Vector3 buildingOffset;
    private Vector3 buildingClickOffset;
    private bool canRotate;

    GameObject[] grassObjects;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
  
    private void Start()
    {
        string tilepath = @"Tiles/";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilepath + "white"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilepath + "red"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilepath + "green"));
        grassObjects = GameObject.FindGameObjectsWithTag("Grass");

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
    public void InitializeWithBuilding(TypeBuild build)
    {
        buildingTemp = Instantiate(build.build, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        buildingOffset = build.placeOffSet;
        buildingClickOffset = build.clickOffSet;
        if (build.build.ToString() == "Wall") canRotate = true; else canRotate = false;
        FollowBuilding();
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
        SetTilesBlock(area, TileType.Empty,tempTilemap);
        SetTilesBlock(area,TileType.Red,mainTilemap);
        EliminateGrass(area, mainTilemap);
    }
    
    public void MoveBuildAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!buildingTemp) return;

        if (!buildingTemp.Placed)
        {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
                {
                    Vector3 worldPoint = hit.point;

                    Vector3Int cellPos = gridLayout.WorldToCell(worldPoint);
                    if (prevPos != cellPos)
                    {
                        buildingTemp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + buildingClickOffset);
                        prevPos = cellPos;
                        FollowBuilding();
                    }
                }
        }
    }
    private void EliminateGrass(BoundsInt area, Tilemap mainTileMap)
    {
        foreach (var obj in grassObjects)
        {
            Vector3Int cell = mainTileMap.WorldToCell(obj.transform.position);

            if (area.Contains(cell))
            {
                GameObject.Destroy(obj);
            }
        }

    }
    public void BuildAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!buildingTemp) return;

        if (buildingTemp.CanBePlaced())
        {
            buildingTemp.Place();
        }

    }
    public void CancelBuildAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!buildingTemp) return;

        ClearArea();
        Destroy(buildingTemp.gameObject);
    }
    public void RotateBuildAction(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!buildingTemp) return;
        if(!canRotate) return;
        buildingTemp.transform.RotateAround(buildingTemp.transform.position, Vector3.up, 90f);
    }

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }

}
