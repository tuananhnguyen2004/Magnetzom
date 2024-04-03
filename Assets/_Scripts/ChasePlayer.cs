using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateMachineBehaviour
{
    [SerializeField] private float chaseSpeed;
    private GameObject player;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        }

        var localScale = animator.transform.localScale;
        if (player.transform.position.x > animator.transform.position.x)
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        else
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        animator.transform.localScale = localScale;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
