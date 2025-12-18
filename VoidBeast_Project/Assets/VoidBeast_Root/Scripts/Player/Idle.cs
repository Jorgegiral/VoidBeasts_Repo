using UnityEngine;

public class Idle : StateMachineBehaviour
{
    [SerializeField]private float timetillIdle;
    [SerializeField] private int numberOfIdle;
    private bool nextIdle;
    private float idleTime;
    private int idleAnimation;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nextIdle == false)
        {
            idleTime += Time.deltaTime;

            if (idleTime > timetillIdle && stateInfo.normalizedTime % 1 < 0.1f)
            {
                nextIdle = true;
                idleAnimation = Random.Range(1, numberOfIdle + 1);

            }
        }
        else if(stateInfo.normalizedTime % 1 > 0.85)
        {
            ResetIdle();
        }
        animator.SetFloat("IdleAnim", idleAnimation, 0.2f, Time.deltaTime);
    }
    private void ResetIdle()
    {
        nextIdle = false;
        idleTime = 0;
        idleAnimation = 0;
    }
}
