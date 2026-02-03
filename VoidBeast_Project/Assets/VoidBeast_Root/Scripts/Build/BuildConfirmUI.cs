using UnityEngine;

public class BuildConfirmUI : MonoBehaviour
{
    public static BuildConfirmUI instance;
    [SerializeField] private Vector3 screenOffset;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (GridBuilding.instance == null) return;
        if (GridBuilding.instance.buildingTemp == null) return;
        checkOffset();
        Transform target = GridBuilding.instance.buildingTemp.transform;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        transform.position = screenPos + screenOffset;
    }
    private void checkOffset()
    {
        if (GridBuilding.instance.isTower)
        {
            screenOffset = new Vector3(0, 150, 0);
        }
        if (GridBuilding.instance.isWall)
        {
            screenOffset = new Vector3(10, 125, 0);

        }
        if (GridBuilding.instance.isBuild)
        {
            screenOffset = new Vector3(120, 200, 0);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
