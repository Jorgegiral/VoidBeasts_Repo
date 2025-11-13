using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedShop : MonoBehaviour
{

    void Update()
    {
        PlayerStats.instance.blockMovement = true;
    }

}
