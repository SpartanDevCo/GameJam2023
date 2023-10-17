using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EarthBoss : MonoBehaviour, IDamageable
{
    [Header("Atributos")]
    public float life = 100;
    public float timeBtwAttacks = 2f;
    public float stopDistance = 10f;
    public float distanceToPlayer;
    public bool beAlert;
    public float alertRange = 40;
    public bool found = false;
    public bool Near = false;


    [Header("Referencias")]
    private GameObject boss;
    public Transform target;
    public LayerMask playerLayer;
    public GameObject rocks;
    public GameObject rainingRocks;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnPoint2;
    public Animator anim;
    public Slider lifebar;

    void Start()
    {
        lifebar.maxValue = 100;
        lifebar.value = 100;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //RotationTowardsPlayer();
        CheckDistanceAndAttack();
        
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //found = distanceToPlayer <= alertRange;
        if (distanceToPlayer <= alertRange)
        {
            found = true;
        }

        Near = distanceToPlayer <= stopDistance;

        BeAlert();
    }
    private void OnDestroy()
    {
        lifebar.gameObject.SetActive(false);
    }

    public void RockAttack(){
        Instantiate(rocks,spawnPoint.position + transform.forward *2, Quaternion.Euler(transform.rotation.eulerAngles.x + 30f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }

    public void RainingRocksAttack(){
        Debug.Log("Raining Rocks");
        GameObject rainingRock = Instantiate(rainingRocks, spawnPoint2.position, Quaternion.identity);
        Vector3 directionToPlayer = (target.position - rainingRock.transform.position).normalized;
        rainingRock.transform.forward = directionToPlayer;

        Rigidbody rockRb = rainingRock.GetComponent<Rigidbody>();
        if (rockRb != null)
        {
            rockRb.AddForce(directionToPlayer * 10, ForceMode.Impulse);
        }
    }

    public void TakeDamage(float damage){
        life -= damage;
        lifebar.value = life;
        Debug.Log("EarthBoss life: " + life);
        if (life <= 0){
            //animator.SetBool("isDead", true);
            Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
            p.availableAttacks.Add(Player.AttackType.Rock);
            anim.SetBool("dying", true);
            lifebar.gameObject.SetActive(false);    
            p.hp = 100;
            p.heathbar.value = 100;
            Destroy(gameObject, 5f);
        }
    }

    public void BeAlert()
    {
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);

        if (beAlert == true)
        {
            lifebar.gameObject.SetActive(true);
            if (!found)
            {
                boss = GameObject.FindGameObjectWithTag("Boss");
                //cine.AddMember(boss.transform, 1, 1);
                found = true;
            }
            if (!Near)
            {
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            }
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    public void CheckDistanceAndAttack()
    {
        if (Vector3.Distance(transform.position, target.position) <= 10)
        {
            Near = true;
        }
        else
        {
            Near = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}
