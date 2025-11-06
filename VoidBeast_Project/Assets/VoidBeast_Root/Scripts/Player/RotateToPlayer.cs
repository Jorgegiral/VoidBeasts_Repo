using Newtonsoft.Json;
using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.UIElements;

public class RotateToPlayer : MonoBehaviour
{
    public Camera cam;
    public Camera camGameObject;
    private Vector3 shootDirection;
    private Quaternion rotation;
    private LayerMask layerGround;
    [SerializeField] private float rotationSpeed = 5f;
    private Coroutine rotateCoroutine;
    private Vector3 lastHitPoint;

    private void Start()
    {
        layerGround = LayerMask.GetMask("Ground");

    }
    public void RotateOnShoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
        {
            Debug.Log("giro");
            Vector3 lookDirection = (hit.point - transform.position).normalized;
            lookDirection.y = 0f; 
            transform.rotation = Quaternion.LookRotation(lookDirection);
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
