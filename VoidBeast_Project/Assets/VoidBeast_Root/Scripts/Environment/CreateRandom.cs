using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
public class CreateRandom : MonoBehaviour
{
    public GameObject[] enviroElements;
    [SerializeField] int density;
    [SerializeField, UnityEngine.Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;
    [SerializeField] Vector3 minScale;
    [SerializeField] Vector3 maxScale;
    [SerializeField] Vector3 centerPoint = Vector3.zero;
    [SerializeField] float forbiddenRadius = 3f;
    private void Start()
    {
        Generate();
    }
#if UNITY_EDITOR
    [ContextMenu("Generate")]

    public void Generate()
    {
        Clear();
        for (int i = 0; i < density; i++)
        {
            float sampleX = Random.Range(xRange.x, xRange.y);
            float sampleY = Random.Range(zRange.x, zRange.y);
            Vector3 rayStart = new Vector3(sampleX, 1, sampleY);
            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
                continue;
            if (hit.point.y < 0)
                continue;
            if (Vector3.Distance(hit.point, centerPoint) < forbiddenRadius)
                continue;
            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(this.enviroElements[Random.Range(0,enviroElements.Length)], transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x,rotationRange.y),Space.Self);
            instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
                );
        };
    }
    [ContextMenu("Clear")]

    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
#endif

}
