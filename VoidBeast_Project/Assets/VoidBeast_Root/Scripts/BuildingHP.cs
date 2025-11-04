using System.Collections.Generic;
using UnityEngine;

public class BuildingHP : MonoBehaviour
{
    [SerializeField] int buildHP;
    [SerializeField] Canvas deathCanvas;
    public int numberOfAttackPoints = 12;     
    public float attackRadius = 3f;          

    private List<Vector3> attackPoints = new List<Vector3>();
    private List<bool> attackPointOccupied = new List<bool>();
    private void Awake()
    {
        deathCanvas.gameObject.SetActive(false);
        GenerateAttackPoints();


    }
    public void TakeDamage(int damage)
    {
        buildHP -= damage;
        if (buildHP < 0 )
        {
            deathCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

    }
    void GenerateAttackPoints()
    {
        attackPoints.Clear();
        attackPointOccupied.Clear();

        for (int i = 0; i < numberOfAttackPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfAttackPoints;
            Vector3 point = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * attackRadius;
            point += transform.position;
            attackPoints.Add(point);
            attackPointOccupied.Add(false);
        }
    }
    public bool GetFreeAttackPoint(Vector3 enemyPos, out Vector3 point)
    {
        float minDist = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < attackPoints.Count; i++)
        {
            if (!attackPointOccupied[i])
            {
                float dist = Vector3.Distance(enemyPos, attackPoints[i]);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestIndex = i;
                }
            }
        }

        if (closestIndex >= 0)
        {
            attackPointOccupied[closestIndex] = true;
            point = attackPoints[closestIndex];
            return true;
        }

        point = Vector3.zero;
        return false;
    }

    public void ReleaseAttackPoint(Vector3 position)
    {
        for (int i = 0; i < attackPoints.Count; i++)
        {
            if (Vector3.Distance(attackPoints[i], position) < 0.5f)
            {
                attackPointOccupied[i] = false;
                break;
            }
        }
    }
  
}

