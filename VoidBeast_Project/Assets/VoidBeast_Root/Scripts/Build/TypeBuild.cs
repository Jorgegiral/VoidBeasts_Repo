using Unity.VisualScripting;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TypeBuild", menuName = "Scriptable Objects/TypeBuild")]
public class TypeBuild : ScriptableObject
{
    public GameObject build;
    public GameObject[] levelModels;
    public Vector3 clickOffSet;
    public Vector3 placeOffSet;
    public BuildType type;
    public event Action OnBuildChanged;

    public void Upgrade(int level)
    {
        build = levelModels[level];
        OnBuildChanged?.Invoke();
    }
    public enum BuildType
    {
        Tower,
        Wall,
        Build
    }
}
