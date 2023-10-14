using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBoss_Attack2 : StateMachineBehaviour
{
    AirBoss boss;
    public int currentPoint;
    float timer;
    bool spawn;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss == null)
        {
            boss = animator.GetComponent<AirBoss>();

        }
        currentPoint = 0;
        timer = 0;
        spawn = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currentPoint <= 9)
        {
            boss.anim.SetBool("run", true);
            boss.anim.SetBool("idle", false);
            float distance = Vector3.Distance(animator.transform.position, boss.PatrolPoints[currentPoint].target.position);

            if (distance > 0.5f)
            {
                animator.transform.Translate(0, 0, -50 * Time.deltaTime);
            }
            else
            {

                //animator.transform.rotation = boss.PatrolPoints[currentPoint].newRotation;
                animator.transform.Rotate(0, 0, 0);
                animator.transform.rotation = Quaternion.Euler(0, boss.PatrolPoints[currentPoint].newRotation.eulerAngles.y, 0);
                currentPoint++;
            }
        }
        else
        {
            if (spawn)
            {
                boss.Tornado();
                spawn = false;
            }
           
            if (timer <= 10)
            {
                timer += Time.deltaTime;
            }
            else
            {
                boss.anim.SetBool("run", false);
                boss.anim.SetBool("idle", true);
                animator.SetTrigger("changeState");
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
[System.Serializable]
public class TargetInfo
{
    public Quaternion newRotation;
    public Transform target;
}
