using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private Vector3Int buildingOffset = new Vector3Int(-2, -2, 0);


    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        areaTemp.position += buildingOffset;
        if (GridBuilding.instance.CanTakeAre(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Place()
    {
        Vector3Int positionInt = GridBuilding.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        areaTemp.position += buildingOffset;
        GridBuilding.instance.TakeArea(areaTemp);
    }
}
