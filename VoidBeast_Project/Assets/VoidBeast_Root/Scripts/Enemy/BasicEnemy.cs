using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask buildLayer;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] int enemyDamage;
    [SerializeField] private float minSpeed = 0.6f;
    [SerializeField] private float maxSpeed = 2f;

    [Header("Detection prio")]
    [SerializeField] float attackRange;

    private float attackCD = 2;
    private bool canAttack = true;
    private Vector3 assignedAttackPoint;
    private bool hasAttackPoint = false;
    private Animator anim; //Jorge

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); //Jorge
        agent.speed = Random.Range(minSpeed, maxSpeed);

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
        if (!hasAttackPoint)
        {
            BuildingHP building = target.GetComponent<BuildingHP>();
            if (building)
            {
                Vector3 newPoint;
                bool found = building.GetFreeAttackPoint(transform.position, out newPoint);

                if (found)
                {
                    if (NavMesh.SamplePosition(newPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                        assignedAttackPoint = hit.position;
                    else
                        assignedAttackPoint = newPoint;

                    hasAttackPoint = true;
                    agent.SetDestination(assignedAttackPoint);
                }
                else
                {
                    agent.SetDestination(target.transform.position );
                    LookAtBuilding();
                    return;
                }
            }

        }
        float distToPoint = Vector3.Distance(transform.position, assignedAttackPoint);

        if (distToPoint > 2f)
        {
            agent.isStopped = false;
            agent.SetDestination(assignedAttackPoint);
            anim.SetBool("isAttacking", false);

        }
        else
        {
            agent.isStopped = true;
            LookAtBuilding();


            AttackEnemy();
        }
       
    }
    void AttackEnemy()
    {
        anim.SetBool("isAttacking", true);
        if (!canAttack) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, buildLayer))
        {

            var health = hit.collider.GetComponent<BuildingHP>();
            if (health != null)
            {
                health.TakeDamage(enemyDamage);
            }
        }
        canAttack = false;
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
    void LookAtBuilding()
    {
        if (!target) return;

        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f; 

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
   
}
