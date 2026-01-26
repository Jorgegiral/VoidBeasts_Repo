using System.Collections;
using UnityEngine;

public class TakeAreaEnviro : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    public Vector3 offset;
    private BoundsInt occupiedArea;


    public void Place()
    {
        if (GridBuilding.instance == null) return;

        var grid = GridBuilding.instance.gridLayout;

        Vector3Int cellPos = grid.WorldToCell(transform.position);
        Vector3 snappedWorldPos = grid.CellToWorld(cellPos) + grid.cellSize / 2f;
        transform.position = snappedWorldPos;
        transform.position += offset;
        area.position = cellPos;

        Placed = true;
        GridBuilding.instance.TakeArea(area);
        SetArea(area);
    }
    public void SetArea(BoundsInt area)
    {
        occupiedArea = area;
    }
    public void Destroyed()
    {
        GridBuilding.instance.UnTakeArea(occupiedArea);

    }
    public bool CanBePlaced()
    {
        return GridBuilding.instance.CanTakeAre(area);
    }
    private IEnumerator Start()
    {
        yield return null; 
        Place();
    }
}
