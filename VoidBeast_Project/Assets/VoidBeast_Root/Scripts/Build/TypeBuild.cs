using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeBuild", menuName = "Scriptable Objects/TypeBuild")]
public class TypeBuild : ScriptableObject
{
    public GameObject build;
    public GameObject[] levelModels;
    public Vector3 clickOffSet;
    public Vector3 placeOffSet;
    public BuildType type;
    public int level;

    private void Awake()
    {
        CheckLevel();
        CheckModel();
    }
    private void CheckLevel()
    {
        if (type.ToString() == "Build") return;

        if (type.ToString() == "Tower") level = UpgradeManager.instance.towerLevel;
        if (type.ToString() == "Wall") level = UpgradeManager.instance.wallLevel;
    }
    private void CheckModel()
    {
        if (type.ToString() == "Build") return;
        if (level == 1) build = levelModels[0];
        if (level == 2) build = levelModels[1];
        if (level == 3) build = levelModels[2];

    }
    public enum BuildType
    {
        Tower,
        Wall,
        Build
    }
}
