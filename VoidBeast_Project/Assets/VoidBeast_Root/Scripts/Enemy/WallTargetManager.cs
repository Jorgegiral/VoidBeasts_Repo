using UnityEngine;

public class WallTargetManager : MonoBehaviour
{
    public static WallTargetManager Instance;
    public Transform currentWallTarget;
    private void Awake()
    {
        Instance = this;
    }

    public void SetWall(Transform wall)
    {
        if (currentWallTarget == null)
            currentWallTarget = wall;
    }

    public void ClearWall(Transform wall)
    {
        if (currentWallTarget == wall)
            currentWallTarget = null;
    }
}
