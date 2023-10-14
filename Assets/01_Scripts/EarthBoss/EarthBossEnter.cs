using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossEnter : StateMachineBehaviour
{
    EarthBoss earthBoss;
    public float speed;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (earthBoss == null) earthBoss = animator.GetComponent<EarthBoss>();
        speed = 3;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (earthBoss.distanceToPlayer > earthBoss.stopDistance)
       {
            animator.transform.Translate(0, 0, speed * Time.deltaTime);
            earthBoss.anim.SetBool("walk", true);
            earthBoss.anim.SetBool("idle", false);
        }
        else
        {
            animator.SetTrigger("ChangeState");
            earthBoss.anim.SetBool("walk", false);
            earthBoss.anim.SetBool("idle", true);
        }
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
