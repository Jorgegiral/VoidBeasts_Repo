using UnityEngine;

[CreateAssetMenu(fileName = "TypeBuild", menuName = "Scriptable Objects/TypeBuild")]
public class TypeBuild : ScriptableObject
{
    public GameObject build;
    public Vector3 clickOffSet;
    public Vector3 placeOffSet;
}
