using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [Header("Atributos")]
    public float life = 3;
    public float attackRange = 13f;
    bool targetInRange = false;
    [Header("Referencias")]
    public Rigidbody rb;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetInRange){
            RotationTowardsPlayer();
        }
        else
        {
            LookUpForTarget();
        }
    }

    void LookUpForTarget(){
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if(distance <= attackRange)
            {
                targetInRange = true;
            }
        }
    }

    void RotationTowardsPlayer(){
        Vector3 dir = target.position - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 0;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }

    public void TakeDamage(float damage){
        life -= damage;
        Debug.Log("Enemy life: " + life);
        if (life <= 0){
            
            Destroy(gameObject);
        }
    }
}

