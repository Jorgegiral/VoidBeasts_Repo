using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private LayerMask layerGround;

    private void Awake()
    {
        if (instance == null) { instance = this; }

    }
    private void Update()
    {
        if (!buildingTemp)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return ;
            }
            if(!buildingTemp.Placed)
            {
                    Vector3 mousePosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
                    {
                    Vector3Int cellPos = gridLayout.LocalToCell(hit.point);
                    if (prevPos != cellPos)
                    {
                        buildingTemp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(5f, 5f, 0f));
                        prevPos = cellPos;
                    }
                }
            }
        }
    }
    private void Start()
    {
        string tilepath = @"Tiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(path:tilepath + "white"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(path: tilepath + "red"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(path: tilepath + "green"));
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
    public void InitializeWithBuilding(GameObject building)
    {
        buildingTemp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
    }
    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
}
