using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask[] attackLayer;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] int enemyDamage;
    [SerializeField] private float minSpeed = 0.6f;
    [SerializeField] private float maxSpeed = 2f;
    private BuildingHP targetBuilding;


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
        UpdateAttackCooldown();

    }
    void UpdateEnemyTarget()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plants");
        GameObject closestPlant = null;
        float minDist = Mathf.Infinity;

        foreach (var plant in plants)
        {
            float dist = Vector3.Distance(transform.position, plant.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestPlant = plant;
            }
        }

        if (closestPlant != null)
        {
            target = closestPlant.transform;
            targetBuilding = null; 
            hasAttackPoint = false;
        }
        else
        {
            GameObject mainBuilding = GameObject.Find("MainBuild");
            target = mainBuilding.transform;
        }

    }
    void MoveEnemyBuild()
    {
        if (!target) return;

        if (target.CompareTag("MainBuild"))
        {
            BuildingHP building = target.GetComponent<BuildingHP>();
            if (!hasAttackPoint && building)
            {
                if (building.GetFreeAttackPoint(transform.position, out Vector3 newPoint))
                {
                    assignedAttackPoint = NavMesh.SamplePosition(newPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas)
                        ? hit.position
                        : newPoint;
                    targetBuilding = building;
                    hasAttackPoint = true;
                }
                else
                {
                    assignedAttackPoint = target.position;
                }
            }
        }
        else
        {
            assignedAttackPoint = target.position; // Planta
        }

        // Movimiento
        float dist = Vector3.Distance(transform.position, assignedAttackPoint);
        if (dist > 1.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(assignedAttackPoint);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            agent.isStopped = true;
            LookAtTarget();
            AttackTarget();
        }
    }



    void AttackTarget()
    {
        anim.SetBool("isAttacking", true);
        if (!canAttack) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, attackLayer[0]))
        {

            var health = hit.collider.GetComponent<BuildingHP>();
            if (health != null)
            {
                health.TakeDamage(enemyDamage);
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, attackLayer[1]))
        {

            var health = hit.collider.GetComponent<PlantHP>();
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
    void LookAtTarget()
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
    public void OnDeath()
    {
        if (targetBuilding && hasAttackPoint)
        {
            targetBuilding.ReleaseAttackPoint(assignedAttackPoint);
        }
        Destroy(gameObject); 
    }
}
