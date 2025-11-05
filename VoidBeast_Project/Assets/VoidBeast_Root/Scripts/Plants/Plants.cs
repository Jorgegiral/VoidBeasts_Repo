using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Plants", menuName = "Scriptable Objects/Plants")]
public class Plants : ScriptableObject
{
    public int plantName;
    public int numDias;
    public int precio;
    public Image shopImage;
    public List<GameObject> plantGameObject;
    public int ganancias;
}
