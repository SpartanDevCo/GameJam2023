using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    [Header("Atributos")]
    [SerializeField] float hp = 100;
    [SerializeField] float speed = 7;
    [SerializeField] float jumpSpeed = 7;
    [SerializeField] float rayDistance = 10;
    public AttackType attackType = AttackType.Melee;
    public List<AttackType> availableAttacks = new List<AttackType>(){AttackType.Melee};
    bool dead = false;
    float distanceToGround;

    [Header("Referencias")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject rocks;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject slash;
    [SerializeField] GameObject waterBeam;
    [SerializeField] LayerMask interactMask;
    

    [Header("Animaciones")]
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }


    // Update is called once per frame
    void Update()
    {
        if(!dead){
            Move();
            Jump();
            Rotate();
            SearchInteract();
            if(Input.GetMouseButtonDown(0)){SetAnimAttack();}
        }
        
        
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        anim.SetFloat("run", Mathf.Abs(x + z));
        transform.Translate(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed);
    }

    void Rotate(){
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Vector3 direction = mouseOnScreen - positionOnScreen;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }

    bool IsGrounded() {
        return Physics.BoxCast(transform.position, new Vector3(0.4f, 0f, 0.4f),Vector3.down, Quaternion.identity, distanceToGround + 0.3f);
    }

    public void RestoreJump(){
        anim.SetTrigger("Jumping");
    }
    
    void Jump(){
        
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            anim.SetBool("Jumping",true);
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void SearchInteract(){
        UnityEngine.Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, interactMask) && Input.GetKeyDown(KeyCode.E)){
            UnityEngine.Debug.Log("SE PUEDE INTERACTUAR");
            anim.SetInteger("Cinematic",1);
            hit.collider.GetComponent<Animator>().SetTrigger("Activate");
        }
    }

    public void ReturnCinematicNormal(){
        anim.SetInteger("Cinematic",0);
    }
    public void SetAnimAttack(){
        switch (attackType)
        {
            case AttackType.Rock:
                anim.SetInteger("Special",1);
                break;
            case AttackType.Wind:
                anim.SetInteger("Special",2);
                break;
            case AttackType.Water:
                anim.SetInteger("Special",3);
                break;
        }
    }

    public void Attack(){
        switch (attackType)
        {
            case AttackType.Rock:
                RockAttack();
                break;
            case AttackType.Wind:
                AirAttack();
                break;
            case AttackType.Water:
                WaterAttack();
                break;
        }
    }

    public void ReturnToNormal(){
        anim.SetInteger("Special",0);
    }

    void RockAttack(){
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

    void AirAttack(){
        Instantiate(slash,spawnPoint.position + transform.forward *2, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y  + 180f, transform.rotation.eulerAngles.z));
    }

    void WaterAttack(){
        Instantiate(waterBeam,spawnPoint.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }

    public void TakeDamage(float damage)
    {
        if(!dead){
            hp-=damage;
            UnityEngine.Debug.Log("DAÑO AL JUGADOR HP= " + hp);
            if(hp<=0){
                dead = true;
                anim.SetTrigger("Dead");
            }
        }
        
    }

    public enum AttackType{
        Melee,
        Rock,
        Water,
        Wind,
        Fire
    }

    
}
