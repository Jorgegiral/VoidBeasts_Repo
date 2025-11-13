using UnityEngine;
using UnityEngine.EventSystems;

public class DeathCanvasMando : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedOnOpen;
    
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedOnOpen);
    }

}
