using UnityEngine;

public class Idle : StateMachineBehaviour
{
 /*   [SerializeField] private float timeToSecondIdle = 8f;

    private float timer;
    private bool playingSecondIdle;
    private bool secondIdleStarted;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        playingSecondIdle = false;
        secondIdleStarted = false;

        animator.SetFloat("IdleAnim", 0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playingSecondIdle)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSecondIdle)
            {
                playingSecondIdle = true;
                secondIdleStarted = false;
                animator.SetFloat("IdleAnim", 1);
            }
        }
        else
        {
            if (!secondIdleStarted && stateInfo.normalizedTime < 0.1f)
            {
                secondIdleStarted = true;
            }

            if (secondIdleStarted && stateInfo.normalizedTime >= 1f)
            {
                playingSecondIdle = false;
                timer = 0f;
                animator.SetFloat("IdleAnim", 0);
            }
        }
    }*/
}
