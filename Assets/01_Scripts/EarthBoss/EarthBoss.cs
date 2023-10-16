using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss : MonoBehaviour, IDamageable
{
    [Header("Atributos")]
    public float life = 100;
    public float timeBtwAttacks = 2f;
    public float stopDistance = 10f;
    public float distanceToPlayer;

    [Header("Referencias")]
    public Transform target;
    public GameObject rocks;
    public GameObject rainingRocks;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform spawnPoint2;
    public Animator anim;

    //[Header("Animaciones")]
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotationTowardsPlayer();
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    public void RockAttack(){
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit))
        {
            // Verifica si el rayo golpea algo
            if (hit.collider != null)
            {
                // La posición del suelo donde el rayo golpea
                Vector3 groundPosition = hit.point;

                // Spawnear el objeto al nivel del suelo
                Instantiate(rocks,groundPosition + transform.forward *2, Quaternion.Euler(transform.rotation.eulerAngles.x + 30f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            }
        }
    }

    public void RainingRocksAttack(){
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint2.position, Vector3.down, out hit))
        {
            // Verifica si el rayo golpea algo
            if (hit.collider != null)
            {
                // La posición del suelo donde el rayo golpea
                //Vector3 groundPosition = hit.point;
                GameObject rainingRock = Instantiate(rainingRocks, spawnPoint2.position, Quaternion.identity);

                // Apuntar la roca hacia el jugador
                Vector3 directionToPlayer = (target.position - rainingRock.transform.position).normalized;
                rainingRock.transform.forward = directionToPlayer;

                // Lanzar la roca hacia el jugador
                Rigidbody rockRb = rainingRock.GetComponent<Rigidbody>();
                if (rockRb != null)
                {
                    rockRb.AddForce(directionToPlayer * 40, ForceMode.Impulse);
                }
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
        Debug.Log("EarthBoss life: " + life);
        if (life <= 0){
            //animator.SetBool("isDead", true);
            anim.SetBool("dying", true);
            Destroy(gameObject, 5f);
        }
    }
}
