using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotateToPlayer : MonoBehaviour
{
    public Camera cam;
    public Camera camGameObject;
    private Vector3 shootDirection;
    private Quaternion rotation;
    private LayerMask layerGround;
    private Vector3 lastHitPoint;
    [SerializeField] private InputActionReference lookAction; 

    private void Start()
    {
        layerGround = LayerMask.GetMask("Ground");

    }
    public void RotateOnShoot()
    {
        Vector2 stickInput = lookAction.action.ReadValue<Vector2>();
        bool usingGamepad = Gamepad.current != null && stickInput.sqrMagnitude > 0.1f;
        if (usingGamepad)
        {
            Vector3 lookDir = new Vector3(stickInput.x, 0f, stickInput.y);

            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = targetRotation;
            }
        }
        else
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            {
                Vector3 lookDirection = (hit.point - transform.position).normalized;
                lookDirection.y = 0f;
                transform.rotation = Quaternion.LookRotation(lookDirection);
                lastHitPoint = hit.point;
            }
        }
    }


    public Vector3 GetLastHitPoint()
    {
        return lastHitPoint;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
}
