using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    public GameObject destroyDebrisCollider;
    private BoundsInt occupiedArea;

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
        if (destroyDebrisCollider != null)
        {
            StartCoroutine(debrisCD());
        }
        SetArea(areaTemp);
    }
    public void SetArea(BoundsInt area)
    {
        occupiedArea = area;
    }
    public void Destroyed()
    {
        SetArea(area);
        GridBuilding.instance.UnTakeArea(occupiedArea);
        UpgradeManager.instance.wallAvailable++;
        UpgradeManager.instance.wallBought--;
        Destroy(gameObject);
    }
    public void DestroyedTower()
    {
        SetArea(area);
        GridBuilding.instance.UnTakeArea(occupiedArea);
        UpgradeManager.instance.towerAvailable++;
        UpgradeManager.instance.towerBought--;
        Destroy(gameObject);
    }
    IEnumerator debrisCD()
    {
        destroyDebrisCollider.SetActive(true);
        yield return new WaitForSeconds(1f);
        destroyDebrisCollider.SetActive(false);

    }
}
