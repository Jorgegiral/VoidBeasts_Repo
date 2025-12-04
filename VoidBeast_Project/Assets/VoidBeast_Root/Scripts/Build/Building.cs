using Unity.Android.Gradle;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    [SerializeField] TypeBuild buildOffSet;

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        areaTemp.position += buildOffSet.placeOffSetBuilding;
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
        areaTemp.position += buildOffSet.placeOffSetBuilding;
        Placed = true;
        GridBuilding.instance.TakeArea(areaTemp);
    }
}
