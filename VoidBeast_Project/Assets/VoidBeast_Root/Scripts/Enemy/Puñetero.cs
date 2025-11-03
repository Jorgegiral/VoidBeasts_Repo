using UnityEngine;
using UnityEngine.AI;

public class Puñetero : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] int enemyDamage;
    [SerializeField] private float minSpeed = 0.6f;
    [SerializeField] private float maxSpeed = 2f;

    [Header("Detection prio")]
    [SerializeField] float attackRange;

    private float attackCD = 2;
    private bool canAttack = true;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
    }
    void MoveEnemy()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        agent.SetDestination(target.position);

        if (distance < 2f)
        {
            LookAtPlayer();
            AttackEnemy();
        }


    }
    void AttackEnemy()
    {
        if (!canAttack) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, playerLayer))
        {

            var health = hit.collider.GetComponent<PlayerHP>();
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
    void LookAtPlayer()
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
