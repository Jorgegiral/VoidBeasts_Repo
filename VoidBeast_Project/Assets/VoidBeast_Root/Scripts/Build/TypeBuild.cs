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



    public enum BuildType
    {
        Tower,
        Wall,
        Build
    }
}
