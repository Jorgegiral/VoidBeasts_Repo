using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Plants", menuName = "Scriptable Objects/Plants")]
public class Plants : ScriptableObject
{
    public string plantName;
    public int numDias;
    public int precio;
    public List<GameObject> plantGameObject;
    public int ganancias;
    public bool unlocked;
}
