using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBoss : MonoBehaviour
{
    [Header("Atributos")]
    public float life = 500;
    public float timeBtwAttacks = 2f;

    [Header("Referencias")]
    public Transform target;
    public GameObject rocks;
    public GameObject rainingRocks;
    [SerializeField] Transform spawnPoint;

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
        if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit))
        {
            // Verifica si el rayo golpea algo
            if (hit.collider != null)
            {
                // La posición del suelo donde el rayo golpea
                Vector3 groundPosition = hit.point;
                GameObject rainingRock = Instantiate(rainingRocks, groundPosition, Quaternion.identity);

                // Mover la roca hacia arriba, por encima del jefe
                Vector3 bossTop = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                rainingRock.transform.position = bossTop;

                // Apuntar la roca hacia el jugador
                Vector3 directionToPlayer = (target.position - rainingRock.transform.position).normalized;
                rainingRock.transform.forward = directionToPlayer;

                // Lanzar la roca hacia el jugador
                Rigidbody rockRb = rainingRock.GetComponent<Rigidbody>();
                if (rockRb != null)
                {
                    rockRb.AddForce(directionToPlayer * 10, ForceMode.Impulse);
                }
            }
        }
    }

    
}
