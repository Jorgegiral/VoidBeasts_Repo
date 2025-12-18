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
    bool modelUpdated;

    private void Awake()
    {
        if (rayOrigin == null)
            rayOrigin = transform;
    }
    public void ThrowRaycast()
    {
        if (modelUpdated) { return; }
        modelUpdated = true;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, Vector3.forward, out hit, rayRange,wallLayer))
        {
            northRay = true;
            WallBehaviour wall = hit.collider.GetComponentInParent<WallBehaviour>();
                wall.ThrowRaycast();
            Debug.Log("N");
            
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.right, out hit, rayRange, wallLayer))
        {
            rightRay = true;

            WallBehaviour wall = hit.collider.GetComponentInParent<WallBehaviour>();
            wall.ThrowRaycast();
            Debug.Log("R");

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.left, out hit, rayRange, wallLayer))
        {
            leftRay = true;
            WallBehaviour wall = hit.collider.GetComponentInParent<WallBehaviour>();
            wall.ThrowRaycast();
            Debug.Log("L");

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.back, out hit, rayRange, wallLayer))
        {
            southRay = true;
            WallBehaviour wall = hit.collider.GetComponentInParent<WallBehaviour>();
            wall.ThrowRaycast();
            Debug.Log("S");

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
        switch (key)
        {
            case 0:
                wallModels[0].SetActive(true);
                break;
            case 1:
                wallModels[1].SetActive(true);
                break;
            case 2:
                wallModels[1].SetActive(true);
                break;
            case 4:
                wallModels[0].SetActive(true);
                break;
            case 8:
                wallModels[0].SetActive(true);
                break;
            case 3:
                wallModels[1].SetActive(true);
                break;
            case 7:
                wallModels[8].SetActive(true);
                break;
            case 15:
                wallModels[10].SetActive(true);
                break;
            case 6:
                wallModels[2].SetActive(true);
                break;
            case 10:
                wallModels[4].SetActive(true);
                break;
            case 5:
                wallModels[3].SetActive(true);
                break;
            case 12:
                wallModels[0].SetActive(true);
                break;
            case 9:
                wallModels[5].SetActive(true);
                break;
            case 14:
                wallModels[6].SetActive(true);
                break;
            case 13:
                wallModels[7].SetActive(true);
                break;
            case 11:
                wallModels[9].SetActive(true);
                break;


        }

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
