using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] wallModels;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float rayRange;
    [SerializeField] Transform rayOrigin;

    [SerializeField] List<GameObject> hittedWalls = new List<GameObject>();
    [SerializeField] int key;
    bool northRay;
    bool southRay;
    bool rightRay;
    bool leftRay;
    [SerializeField] int currentWallIndex = 1;
    [SerializeField] int[] wallIndexByKey;
    private void Awake()
    {
        if (rayOrigin == null)
            rayOrigin = transform;
    }
    public void ThrowRaycast()
    {
        northRay = false;
        southRay = false;
        rightRay = false;
        leftRay = false;
        hittedWalls.Clear();

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, Vector3.forward, out hit, rayRange,wallLayer))
        {
            northRay = true;
            hittedWalls.Add(hit.collider.gameObject);
            Debug.Log("N Parent");

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.right, out hit, rayRange, wallLayer))
        {
            rightRay = true;
            hittedWalls.Add(hit.collider.gameObject);
            Debug.Log("R Parent");

        }
        if (Physics.Raycast(rayOrigin.position, Vector3.left, out hit, rayRange, wallLayer))
        {
            leftRay = true;
            hittedWalls.Add(hit.collider.gameObject);
            Debug.Log("L Parent");
        }
        if (Physics.Raycast(rayOrigin.position, Vector3.back, out hit, rayRange, wallLayer))
        {
            southRay = true;
            hittedWalls.Add(hit.collider.gameObject);
            Debug.Log("S Parent");

        }
        ChoseModel();
        ChangeModelNeighbours();
    }
    public void ThrowRaycastNeighbours()
    {
        hittedWalls.Clear();
        northRay = false;
        southRay = false;
        rightRay = false;
        leftRay = false;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayRange, wallLayer))
        {
            northRay = true;
            Debug.Log("N neigh");
            

        }
        if (Physics.Raycast(rayOrigin.position, rayOrigin.right, out hit, rayRange, wallLayer))
        {
            rightRay = true;
            Debug.Log("R neigh");

        }
        if (Physics.Raycast(rayOrigin.position, -rayOrigin.right, out hit, rayRange, wallLayer))
        {
            leftRay = true;
            Debug.Log("L neigh");

        }
        if (Physics.Raycast(rayOrigin.position, -rayOrigin.forward, out hit, rayRange, wallLayer))
        {
            southRay = true;
            Debug.Log("S neigh");

        }
        ChoseModel();
    }
    public void ChangeModelNeighbours()
    {
        foreach (GameObject wall in hittedWalls)
        {
            WallBehaviour neighbour = wall.GetComponentInParent<WallBehaviour>();
            if (neighbour != null)
            {
                neighbour.ThrowRaycastNeighbours();

            }

        }

    }
    private void ChoseModel()
    {
        if (wallIndexByKey == null || wallIndexByKey.Length != 16)
        {
            Debug.LogError(
                $"WallBehaviour inválido en: {gameObject.name}\n" +
                $"Path: {gameObject.transform.root.name}",
                this
            );
            return;
        }
        key = 0;
        if (northRay) key += 1;
        if (southRay) key += 2;
        if (rightRay) key += 4;
        if (leftRay) key += 8;
        northRay = false;
        southRay = false;
        rightRay = false;
        leftRay = false;
        if (key < 0 || key >= wallIndexByKey.Length)
        {
            Debug.LogError($"Key fuera de rango: {key}");
            return;
        }
        int nextWallIndex = wallIndexByKey[key];

        if (currentWallIndex == nextWallIndex)
            return;
        if (nextWallIndex < 0 || nextWallIndex >= wallModels.Length)
        {
            Debug.LogError($"Índice de muro inválido: {nextWallIndex}");
            return;
        }
        DisableAllModels();
        wallModels[nextWallIndex].SetActive(true);
        currentWallIndex = nextWallIndex;

    }
    void DisableAllModels()
    {
        for (int i = 0; i < wallModels.Length; i++)
        {
            wallModels[i].SetActive(false);
        }
    }
}
