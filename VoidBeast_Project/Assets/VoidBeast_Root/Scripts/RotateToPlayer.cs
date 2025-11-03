using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.UIElements;

public class RotateToPlayer : MonoBehaviour
{
    public Camera cam;
    public Camera camGameObject;
    public float maximumLength;
    private Vector3 shootDirection;
    private Quaternion rotation;
    private LayerMask layerGround;


    private void Start()
    {
        layerGround = LayerMask.GetMask("Ground");

    }
    public void RotateOnShoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 camRotation = camGameObject.transform.position - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(mousePosition,camRotation,out hit, Mathf.Infinity, layerGround))
        {
            transform.rotation = Quaternion.LookRotation(hit.point);
        }

    }
    public void RotateOnStopMoving()
    {
        Ray centerRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 mousePoint = mouseRay.GetPoint(maximumLength);
        Vector3 cameraPoint = centerRay.origin;

        shootDirection = (mousePoint - cameraPoint).normalized;
        shootDirection.y = 0f;
        shootDirection.Normalize();
        rotation = Quaternion.LookRotation(shootDirection);
        float rotationSpeed = 5f;
        Debug.Log("test");

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    public Quaternion GetRotation()
    {
        return rotation;
    }
    public Vector3 GetShotDirection()
    {
        return shootDirection;
    }
}
