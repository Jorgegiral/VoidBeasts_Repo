using UnityEngine;
using UnityEngine.AI;

public class PuñeteroFlyEnemy : MonoBehaviour
{
    [Header("AI Config")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] int enemyDamage = 20;
    [SerializeField] private float minSpeed = 0.6f;
    [SerializeField] private float maxSpeed = 2f;
    private Animator anim;
    [Header("Detection prio")]
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPoint;


    private float attackCD = 2;
    private bool canAttack = true;

    [SerializeField] AudioClip attackEnemySound;
    [SerializeField] AudioClip moveEnemySound;
    private int comboIndex;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(minSpeed, maxSpeed);
        anim = GetComponent<Animator>();
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

        if (distance > 2.0f)
        {
            Settings.instance.PlayUniqueSoundSFXClip(moveEnemySound, transform, 1f);

            agent.isStopped = false;

            agent.SetDestination(target.position);
            anim.SetBool("isAttacking", false);

        }
        else
        {
            agent.isStopped = true;
            LookAtPlayer();
            AttackEnemy();
        }


    }
    void AttackEnemy()
    {
        if (!canAttack) return;
        anim.SetBool("isAttacking", true);
        Settings.instance.PlaySoundFXClip(attackEnemySound, transform, 1f);

        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, transform.forward, out hit, attackRange, playerLayer))
        {

            var health = hit.collider.GetComponent<PlayerHP>();
            if (health != null)
            {
                health.TakeDamage(enemyDamage);
                anim.SetInteger("ComboIndex", 1);
                comboIndex++;
                if (comboIndex == 3)
                {
                    comboIndex = 0;
                    anim.SetInteger("ComboIndex", -3);
                }
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
