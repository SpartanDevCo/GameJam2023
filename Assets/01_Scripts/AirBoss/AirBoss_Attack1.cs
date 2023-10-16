using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBoss_Attack1 : StateMachineBehaviour
{
    AirBoss boss;
    int bulletCount;
    float timer;
    float initialY;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss == null)
        {
            boss = animator.GetComponent<AirBoss>();

        }
        bulletCount = boss.bulletCount;
        timer = 0;
        initialY = animator.transform.position.y;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(bulletCount > 0)
        {
           
            if (animator.transform.position.y < 100f)
            {
                boss.anim.SetBool("fly", true);
                boss.anim.SetBool("idle", false);
                animator.transform.Translate(0, 6 * Time.deltaTime, 0);
            }
            else
            {
                boss.LookAtPlayer();
                if (timer < boss.timeBtwShoot)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    boss.Shoot();
                    timer = 0;
                    bulletCount--;
                }
            }
        }
        else
        {
            if (animator.transform.position.y >= initialY)
            {
                animator.transform.Translate(0, -(6 * Time.deltaTime), 0);
            }
            else
            {
                animator.transform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetInteger("attackType", 0);
                animator.SetTrigger("changeState");
                boss.anim.SetBool("fly", false);
                boss.anim.SetBool("idle", true);
            }
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
