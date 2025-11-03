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
    [SerializeField] float enemySpeed;


    [Header("Detection prio")]
    [SerializeField] float attackRange;
    [SerializeField] float lockRange;

    private bool alreadyAttack;
    private float attackCD;
    private bool canAttack;
    


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        UpdateEnemyTarget();
        MoveEnemy();
        UpdateAttackCooldown();

    }
    void UpdateEnemyTarget()
    {
        GameObject plant = GameObject.FindWithTag("Plants");

        if (plant != null)
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
        float enemyStep = enemySpeed * Time.deltaTime;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, attackRange, buildLayer))
        {
            agent.isStopped = true;
            if (!alreadyAttack)
            {
                AttackEnemy();
            }
        }else
        if (distance < lockRange)
        {
            transform.position = Vector3.MoveTowards(transform.position,target.position,attackRange);
        }
        else if(distance > attackRange) 
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
    private void UpdateAttackCooldown()
    {
        if (!canAttack)
        {
            attackCD -= Time.deltaTime;
            if (attackCD <= 0f)
            {
                canAttack = true;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + transform.forward * attackRange);
    }
}
