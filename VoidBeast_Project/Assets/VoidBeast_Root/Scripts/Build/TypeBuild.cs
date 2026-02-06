using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "TypeBuild", menuName = "Scriptable Objects/TypeBuild")]
public class TypeBuild : ScriptableObject
{
    public GameObject build;
    public GameObject[] levelModels;
    public Vector3 clickOffSet;
    public Vector3 placeOffSet;
    public BuildType type;
    public int precio;
    public event Action OnBuildChanged;
    public LocalizedString nameBuild;
    public LocalizedString info;
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
