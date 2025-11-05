using UnityEngine;

public class SeedShop : MonoBehaviour
{


    void Update()
    {
        PlayerStats.instance.isPlanting = true;
    }
    public void CloseShop()
    {
        PlayerStats.instance.isPlanting = false;
        gameObject.SetActive(false);
    }
}
