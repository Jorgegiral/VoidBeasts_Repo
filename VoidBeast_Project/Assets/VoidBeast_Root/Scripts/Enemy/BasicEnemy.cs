using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] LayerMask targetpriolayer;

    [SerializeField] float timeBetweenAttacks;

    [Header("Detection prio")]
    [SerializeField] float attackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        UpdateEnemyTarget();
    }
    void UpdateEnemyTarget()
    {
        if (target != null)
        {
            if (GameObject.FindWithTag("Plants"))
            {
                //codigo para que vaya a las plantas aún por hacer
            } else
            {
                target = GameObject.Find("MainBuilding").transform;
                agent.SetDestination(target.position);
            }
        }
    }
    void AttackEnemy()
    {
        
    }
}
