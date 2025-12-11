using System.Diagnostics;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] wallModels;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float rayRange;
    [SerializeField] Transform rayOrigin;
    bool northRay;
    bool southRay;
    bool eastRay;
    bool westRay;
    void Start()
    {
        
    }
    public void ThrowRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, transform.forward, out hit, rayRange,wallLayer))
        {

            var wall = hit.collider.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                wall.ThrowRaycast();
            }
        }
    }
    private void ChoseModel()
    {

    }
}
