using UnityEngine;
using UnityEngine.EventSystems;

public class SeedShop : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedMenu;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedMenu);

    }
    void Update()
    {
        PlayerStats.instance.blockMovement = true;
    }
}
