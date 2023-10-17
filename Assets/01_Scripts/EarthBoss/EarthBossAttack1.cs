using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBossAttack1 : StateMachineBehaviour
{
    EarthBoss earthBoss;
    int rockAttacksCount;
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (earthBoss == null) earthBoss = animator.GetComponent<EarthBoss>();
        timer = 0;
        rockAttacksCount = Random.Range(2, 5);
      //   earthBoss.anim.SetBool("idle", false);
      //   earthBoss.anim.SetBool("rocks", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (timer < earthBoss.timeBtwAttacks / 2f)
       {
            timer += Time.deltaTime;
       }
       else
       {
          earthBoss.RockAttack();
          timer = 0;
          rockAttacksCount--;
         //  earthBoss.anim.SetBool("rocks", true);
         //  earthBoss.anim.SetBool("idle", false);
       }
       if (rockAttacksCount == 0)
       {
         //  earthBoss.anim.SetBool("rocks", false);
         //  earthBoss.anim.SetBool("idle", true);
          animator.SetTrigger("ChangeState");
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
