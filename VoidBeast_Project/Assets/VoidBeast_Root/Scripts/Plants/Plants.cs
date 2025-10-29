using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Plants", menuName = "Scriptable Objects/Plants")]
public class Plants : ScriptableObject
{
    int numDias;
    int precio;
    Image shopImage;
    GameObject plantGameObject;
    int ganancias;
}
