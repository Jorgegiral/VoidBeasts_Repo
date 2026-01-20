using UnityEngine;

public class TakeAreaEnviro : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private void Start()
    {
        Place();
    }
    public void Place()
    {
        Placed = true;
        area.position = GridBuilding.instance.gridLayout.WorldToCell(gameObject.transform.position);
        GridBuilding.instance.TakeArea(area);
    }
    public bool CanBePlaced()
    {
        return GridBuilding.instance.CanTakeAre(area);
    }
}
