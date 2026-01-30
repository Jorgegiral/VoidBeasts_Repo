using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] float timeBetweenAttacks;



    [Header("Enemy Parameters")]
    [SerializeField] float enemyDamage;
    [SerializeField] private float minSpeed = 0.6f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] float attackRange;
    [SerializeField] float attackCD = 2;
    [SerializeField] Transform attackPoint;



    [Header("Enemy Sound")]
    [SerializeField] AudioClip attackEnemySound;
    [SerializeField] AudioClip moveEnemySound;


    private bool canAttack = true;
    private Vector3 assignedAttackPoint;
    private bool hasAttackPoint = false;
    private Animator anim; //Jorge
    private BuildingHP targetBuilding;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); //Jorge
        agent.speed = Random.Range(minSpeed, maxSpeed);

    }

    void Update()
    {

        UpdateEnemyTarget(); 
        MoveEnemyBuild();
        UpdateAttackCooldown();

    }
    void UpdateEnemyTarget()
    {
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        GameObject nearestPlant = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject plant in plants)
        {
            float distance = Vector3.Distance(transform.position, plant.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPlant = plant;
            }
        }
        if (nearestPlant != null)
        {
            if (target == null || target != nearestPlant.transform)
            {
                hasAttackPoint = false; 
                targetBuilding = null;
            }
            target = nearestPlant.transform;
        }
        else
        {
            GameObject mainBuilding = GameObject.Find("MainBuild");
            if (target == null || target != mainBuilding.transform)
            {
                hasAttackPoint = false;
            }
            target = mainBuilding.transform;
            targetBuilding = mainBuilding.GetComponent<BuildingHP>();

        }

    }
    void MoveEnemyBuild()
    {
        if (!target) return;

        if (target.name == "MainBuild")
        {
            BuildingHP building = target.GetComponent<BuildingHP>();
            if (!hasAttackPoint && building)
            {
                if (building.GetFreeAttackPoint(transform.position, out Vector3 newPoint))
                {
                    if (NavMesh.SamplePosition(newPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                        assignedAttackPoint = hit.position;
                    else
                        assignedAttackPoint = newPoint;
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
        if (dist > 2.0f)
        {
            Settings.instance.PlayUniqueSoundSFXClip(moveEnemySound, transform, 1f);
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

        Settings.instance.PlaySoundFXClip(attackEnemySound, transform, 1f);
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, transform.forward, out hit, attackRange, attackLayer))
        {

            var health = hit.collider.GetComponent<BuildingHP>();
            var planthealth = hit.collider.GetComponent<PlantHP>();

            if (health != null)
            {
                health.TakeDamage(enemyDamage);
            }
            if (planthealth != null)
            {
                planthealth.TakeDamage(enemyDamage);
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
