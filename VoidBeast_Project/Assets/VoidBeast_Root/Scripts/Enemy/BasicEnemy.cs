using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BasicEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask buildLayer;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] int enemyDamage;

    [Header("Detection prio")]
    [SerializeField] float attackRange;
    private bool alreadyAttack;
    private float attackCD;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        UpdateEnemyTarget();
        MoveEnemy();
    }
    void UpdateEnemyTarget()
    {

            if (GameObject.FindWithTag("Plants"))
            {
                //codigo para que vaya a las plantas aún por hacer
            } else
            {
                GameObject mainBuilding = GameObject.Find("MainBuild");
                target = mainBuilding.transform;
              
            }
        
    }
    void MoveEnemy()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange, buildLayer))
        {
            agent.isStopped = true;
            if (!alreadyAttack)
            {
                AttackEnemy();
            }
        }else
        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
    }
    void AttackEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, buildLayer))
        {

            var health = hit.collider.GetComponent<BuildingHP>();
            if (health != null)
            {
                health.TakeDamage(enemyDamage);
            }
        }
        alreadyAttack = true;
        attackCD = timeBetweenAttacks;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + transform.forward * attackRange);
    }
}
