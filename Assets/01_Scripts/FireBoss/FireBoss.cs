using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class FireBoss : MonoBehaviour
{
    public static float hp;

    #region Alert
    bool beAlert;
    public bool found = false;

    public bool Near = false;

    public float alertRange = 10;
    public LayerMask playerLayer;

    public CinemachineTargetGroup cine;
    private GameObject boss;
    public Animator animator;
    #endregion

    #region Movement
    public Transform player1Target;
    public Transform player2Target;
    public Transform target;
    public float speed = 8;
    bool playerFound = false;
    public Animator anim;

   

    #endregion

    void Update()
    {
        BeAlert();
        //LookAtPlayer();
        CheckDistanceAndAttack();

    }
    public void BeAlert()
    {
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);

        if (beAlert == true)
        {
            if (!found)
            {
                boss = GameObject.FindGameObjectWithTag("Boss");
                //cine.AddMember(boss.transform, 1, 1);
                found = true;
            }
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            playerFound = true;
        }
    }
    public void LookAtPlayer()
    {
        if (playerFound)
        {
            Vector3 dir = target.position - transform.position;
            float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 180;
            transform.rotation = Quaternion.Euler(0, -angleY, 0);
        }

    }
    public void CheckDistanceAndAttack()
    {
        if (Vector3.Distance(transform.position, target.position) <= 10)
        {
            Near = true;
        } else {
            Near = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}
