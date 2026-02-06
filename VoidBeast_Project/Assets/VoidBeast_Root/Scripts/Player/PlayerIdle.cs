using UnityEngine;

public class PlayerIdle : MonoBehaviour
{
    private Animator animator;
    private float timer;
    [SerializeField] private float idleInterval = 8f;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= idleInterval)
        {
            animator.SetTrigger("Idle2");
            timer = 0f;
        }
    }
}

