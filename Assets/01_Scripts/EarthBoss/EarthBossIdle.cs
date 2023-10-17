using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossIdle : StateMachineBehaviour
{
    EarthBoss earthBoss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (earthBoss == null) earthBoss = animator.GetComponent<EarthBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        if (earthBoss.found && earthBoss.distanceToPlayer > earthBoss.stopDistance)
        {
            animator.SetBool("PlayerIn", true);
            animator.SetInteger("AttackType", 0);
        }
        else if (earthBoss.distanceToPlayer <= earthBoss.stopDistance)
        {
            animator.SetTrigger("ChangeState");
            //animator.SetInteger("AttackType", earthBoss.Near ? 1 : 2);
            animator.SetInteger("AttackType", Random.Range(1, 3));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

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
