using System.Collections;
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
    public bool modelUpdated;

    private void Awake()
    {
        if (rayOrigin == null)
            rayOrigin = transform;
    }
    public void ThrowRaycast()
    {
        if (modelUpdated) { return; }
        northRay = false;
        southRay = false;
        rightRay = false;
        leftRay = false;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, Vector3.forward, out hit, rayRange,wallLayer))
        {
            northRay = true;
            
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.right, out hit, rayRange, wallLayer))
        {
            rightRay = true;

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.left, out hit, rayRange, wallLayer))
        {
            leftRay = true;

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.back, out hit, rayRange, wallLayer))
        {
            southRay = true;

        }
        ChoseModel();

        StartCoroutine(UpdateCooldown());
        

    }
    private void ChoseModel()
    {
        int key = 0;
        if (northRay) key += 1;
        if (southRay) key += 2;
        if (rightRay) key += 4;
        if (leftRay) key += 8;

        foreach (GameObject wall in wallModels)
        {
            wall.SetActive(false);
        }
        int[] wallIndexByKey =
        {
            0,  
            1, 
            1,  
            1, 
            0,  
            3,  
            2, 
            8,  
            0,  
            5,  
            4,  
            9,  
            0,  
            7,  
            6,  
            10  
        };
        wallModels[wallIndexByKey[key]].SetActive(true);
        key = 0;
    }
    IEnumerator UpdateCooldown()
    {
        modelUpdated = true;
        yield return new WaitForSeconds(0.1f);
        modelUpdated = false;
    }


}
