using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedShop : MonoBehaviour
{

    void Update()
    {
        if(ParcelaManager.instance.freeSeed) ParcelaManager.instance.freeSeedText.SetActive(true);
        PlayerStats.instance.blockMovement = true;
    }

}
