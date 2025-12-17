using System.Collections;
using System.Diagnostics;
using System.Reflection.Emit;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] wallModels;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float rayRange;
    [SerializeField] Transform rayOrigin;
    bool northRay;
    bool southRay;
    bool rightRay;
    bool leftRay;
    bool modelUpdated;

    void Start()
    {
        
    }
    public void ThrowRaycast()
    {
        if (modelUpdated) { return; }

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, Vector3.forward, out hit, rayRange,wallLayer))
        {
            northRay = true;
            var wall = hit.collider.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                wall.ThrowRaycast();
            }
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.right, out hit, rayRange, wallLayer))
        {
            rightRay = true;

            var wall = hit.collider.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                wall.ThrowRaycast();
            }
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.left, out hit, rayRange, wallLayer))
        {
            leftRay = true;
            var wall = hit.collider.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                wall.ThrowRaycast();
            }
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.back, out hit, rayRange, wallLayer))
        {
            southRay = true;
            var wall = hit.collider.GetComponent<WallBehaviour>();
            if (wall != null)
            {
                wall.ThrowRaycast();
            }
        }
        ChoseModel();
        UpdateCooldown();

    }
    private void ChoseModel()
    {




        northRay = false;
        southRay = false;
        rightRay = false;
        leftRay = false;
    }
    IEnumerator UpdateCooldown()
    {
        modelUpdated = true;
        yield return new WaitForSeconds(0.5f);
        modelUpdated = false;
    }
}
