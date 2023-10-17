using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [Header("Atributos")]
    public float life = 3;
    public float attackRange = 13f;
    bool dead = false;
    bool beAlert = false;
    [SerializeField] float speed = 7;
    bool targetInRange = false;
    [Header("Referencias")]
    public Rigidbody rb;
    public Transform target;
    [SerializeField] Animator anim;

    [SerializeField] LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            if(targetInRange){
            RotationTowardsPlayer();
            }
            else{
                anim.SetFloat("speed",0);
            }
            LookUpForTarget();
        }
        
    }

    void LookUpForTarget(){
        beAlert = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (beAlert == true)
        {
            //Debug.Log("Se ha Encontrado al player");
            if (!targetInRange)
            {
                targetInRange = true;
            }
        }
        else{
            targetInRange = false;
        }
    }

    void RotationTowardsPlayer(){
        Vector3 dir = target.position - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 0;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
        if (Vector3.Distance(transform.position, target.position) <= 5)
        {
            Attack();
        } else {
            anim.SetBool("Attack",false);
            Move();
        }
        
    }
    void Attack(){
        anim.SetBool("Attack",true);
    }

    void Move(){
        transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
        anim.SetFloat("speed",1);
    }

    

    public void TakeDamage(float damage){
        if(!dead){
            life -= damage;
            Debug.Log("Enemy life: " + life);
            if (life <= 0){
                dead = true;
                anim.SetTrigger("Dead");
            }
        }
        
    }

    public void Destroyer(){
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

