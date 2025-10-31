using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.UIElements;

public class RotateToPlayer : MonoBehaviour
{
    public Camera cam;
    public float maximumLength;
    private Vector3 shootDirection;
    private Quaternion rotation;



    public void RotateOnShoot()
    {
        Ray centerRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        Vector3 mousePoint = mouseRay.GetPoint(maximumLength);
        Vector3 cameraPoint = centerRay.origin;

        shootDirection = (mousePoint - cameraPoint).normalized;
        shootDirection.y = 0f;
        shootDirection.Normalize();
        rotation = Quaternion.LookRotation(shootDirection);

        transform.rotation = rotation;
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
