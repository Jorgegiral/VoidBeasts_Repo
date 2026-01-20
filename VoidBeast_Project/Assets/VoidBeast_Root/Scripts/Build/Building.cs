using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    public GameObject destroyDebrisCollider;

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
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
        GridBuilding.instance.TakeArea(areaTemp);
        if(destroyDebrisCollider != null)
        {
            StartCoroutine(debrisCD());
        }
        
    }
    IEnumerator debrisCD()
    {
        destroyDebrisCollider.SetActive(true);
        yield return new WaitForSeconds(1f);
        destroyDebrisCollider.SetActive(false);

    }
}
