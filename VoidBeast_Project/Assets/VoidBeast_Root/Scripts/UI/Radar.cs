using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] Transform sweepTransform;
    float rotationSpeed;
    float radarDistance;
    LayerMask enemyLayer;
    [SerializeField] Transform player;
    private List<Collider> colliderList;
    [SerializeField] Transform radarCamera;
    void Awake()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        rotationSpeed = 70f;
        radarDistance = 19f;
        colliderList = new List<Collider>();
    }

    void Update()
    {
        transform.position = player.position + new Vector3(0,0.25f,0);
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        sweepTransform.Rotate(0,0,-rotationSpeed * Time.deltaTime);
        radarCamera.position = new Vector3(player.position.x,0,player.position.z);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit,radarDistance,enemyLayer))
        {
            hit.collider.GetComponentInChildren<DetectRadar>().active = true;

        }
    }
    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f);
    }
}
