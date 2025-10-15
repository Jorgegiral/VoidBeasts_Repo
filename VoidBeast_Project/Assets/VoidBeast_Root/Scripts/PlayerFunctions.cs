using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFunctions : MonoBehaviour
{
    LayerMask layerInteractable;
    private Vector3 originRaycast = new Vector3(0,0.5f,0);
    
    private void Awake()
    {
        layerInteractable = LayerMask.GetMask("Interactable", "Player");

    }
    void Update()
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position-originRaycast, transform.TransformDirection(Vector3.forward), out hit, 30, layerInteractable))
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hit.distance,Color.green);
            Debug.Log("Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Nope");
        }


    }
}
