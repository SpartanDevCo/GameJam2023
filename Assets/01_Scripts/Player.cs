using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Atributos")]
    [SerializeField] float hp = 100;
    [SerializeField] float speed = 7;
    [SerializeField] float jumpSpeed = 7;
    [SerializeField] float rayDistance = 10;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] Slider heathbar;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.8f;
    float turnSmoothVelocity;
    bool dead = false;
    bool animationInProgress = false;
    float distanceToGround;
    bool grounded;
    Vector3 velocity;
    public AttackType attackType = AttackType.Melee;
    public List<AttackType> availableAttacks = new List<AttackType>() { AttackType.Melee };
    
    [Header("Referencias")]
    [SerializeField] GameObject rocks;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject slash;
    [SerializeField] GameObject waterBeam;
    [SerializeField] LayerMask interactMask;
    [SerializeField] LayerMask fireI;
    [SerializeField] LayerMask waterI;
    [SerializeField] LayerMask windI;
    [SerializeField] ParticleSystem energy;
    [SerializeField] CharacterController controllerMovement;
    [SerializeField] Transform cam;


    [Header("Animaciones")]
    public Animator anim;
    void Start()
    {
        heathbar.maxValue = 100;
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }
    void Update()
    {
        if (!dead && !animationInProgress)
        {
            Move();
            ControlDash();
            SearchInteract();
            if (Input.GetMouseButtonDown(0)) { SetAnimAttack(); }
        }



    }

    void Move()
    {
        grounded = IsGrounded(); ;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        anim.SetFloat("run", Mathf.Abs(Mathf.Abs(x) + Mathf.Abs(z)));

        Vector3 direction = new Vector3(x,0f,z);
        if(direction.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0F,angle,0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controllerMovement.Move(moveDir.normalized*speed * Time.deltaTime);

        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controllerMovement.Move(velocity * Time.deltaTime);
    }

    void ControlDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 1.5f;
        }
    }

    bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.4f, 0f, 0.4f), Vector3.down, Quaternion.identity, distanceToGround + 0.3f);
    }

    public void RestoreJump()
    {
        anim.SetTrigger("Jumping");
    }

    public void ChangeAnimInProgress(bool value)
    {
        animationInProgress = value;
    }

    void SearchInteract()
    {
        UnityEngine.Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);
        RaycastHit hit;
        var particle = energy.main;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, fireI) && Input.GetKeyDown(KeyCode.E) && availableAttacks.Contains(AttackType.Water))
        {
            UnityEngine.Debug.Log("SE PUEDE INTERACTUAR");
            particle.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 162, 191));
            anim.SetInteger("Cinematic", 1);
            hit.collider.GetComponent<Animator>().SetTrigger("Activate");
            Instantiate(energy, spawnPoint.position, Quaternion.Euler(spawnPoint.eulerAngles.x - 20f, spawnPoint.eulerAngles.y, spawnPoint.eulerAngles.z));
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, windI) && Input.GetKeyDown(KeyCode.E) && availableAttacks.Contains(AttackType.Rock))
        {
            UnityEngine.Debug.Log("SE PUEDE INTERACTUAR");
            particle.startColor = new ParticleSystem.MinMaxGradient(new Color(186, 191, 0));
            anim.SetInteger("Cinematic", 1);
            hit.collider.GetComponent<Animator>().SetTrigger("Activate");
            Instantiate(energy, spawnPoint.position, Quaternion.Euler(spawnPoint.eulerAngles.x - 20f, spawnPoint.eulerAngles.y, spawnPoint.eulerAngles.z));
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, waterI) && Input.GetKeyDown(KeyCode.E) && availableAttacks.Contains(AttackType.Wind))
        {
            UnityEngine.Debug.Log("SE PUEDE INTERACTUAR");
            particle.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 255, 0));
            anim.SetInteger("Cinematic", 1);
            hit.collider.GetComponent<Animator>().SetTrigger("Activate");
            Instantiate(energy, spawnPoint.position, Quaternion.Euler(spawnPoint.eulerAngles.x - 20f, spawnPoint.eulerAngles.y, spawnPoint.eulerAngles.z));
        }

    }

    public void ReturnCinematicNormal()
    {
        anim.SetInteger("Cinematic", 0);
    }
    public void SetAnimAttack()
    {
        switch (attackType)
        {
            case AttackType.Rock:
                anim.SetInteger("Special", 1);
                break;
            case AttackType.Wind:
                anim.SetInteger("Special", 2);
                break;
            case AttackType.Water:
                anim.SetInteger("Special", 3);
                break;
        }
    }

    public void Attack()
    {
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

    public void ReturnToNormal()
    {
        anim.SetInteger("Special", 0);
    }

    void RockAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit))
        {
            // Verifica si el rayo golpea algo
            if (hit.collider != null)
            {
                // La posición del suelo donde el rayo golpea
                Vector3 groundPosition = hit.point;

                // Spawnear el objeto al nivel del suelo
                Instantiate(rocks, groundPosition + transform.forward * 2, Quaternion.Euler(transform.rotation.eulerAngles.x + 30f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            }
        }
    }

    void AirAttack()
    {
        Instantiate(slash, spawnPoint.position + transform.forward * 2, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z));
    }

    void WaterAttack()
    {
        Instantiate(waterBeam, spawnPoint.position, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }

    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            hp -= damage;
            heathbar.value = hp;
            UnityEngine.Debug.Log("DAÑO AL JUGADOR HP= " + hp);
            if (hp <= 0)
            {
                dead = true;
                anim.SetTrigger("Dead");
            }
        }

    }

    public enum AttackType
    {
        Melee,
        Rock,
        Water,
        Wind,
        Fire
    }


}
