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
    public void RotateOnStopMoving()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
        {
            Vector3 lookDirection = (hit.point - transform.position).normalized;
            lookDirection.y = 0f;
            Quaternion targetRot = Quaternion.LookRotation(lookDirection);
            if (rotateCoroutine != null)
                StopCoroutine(rotateCoroutine);
            rotateCoroutine = StartCoroutine(SmoothRotate(targetRot));
        }
    }

    private IEnumerator SmoothRotate(Quaternion targetRot)
    {
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRot;
    }
    public void StopRotation()
    {
        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
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
