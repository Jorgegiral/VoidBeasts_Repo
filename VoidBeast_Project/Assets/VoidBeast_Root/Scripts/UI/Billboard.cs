using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    private void LateUpdate()
    {
        transform.LookAt(transform.position,cameraTransform.forward);
    }
}
