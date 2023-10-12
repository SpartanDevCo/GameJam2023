using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] float speed = 7;
    [SerializeField] float jumpSpeed = 7;
    public AttackType attackType = AttackType.Melee;
    float distanceToGround;

    [Header("Referencias")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject rocks;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject slash;
    // Start is called before the first frame update
    void Start()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Rotate();
        if(Input.GetMouseButtonDown(0)){Attack();}
        
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
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

    
    void Jump(){
        
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    void Attack(){
        switch (attackType)
        {
            case AttackType.Rock:
                RockAttack();
                break;
            case AttackType.Wind:
                AirAttack();
                break;
        }
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


    public enum AttackType{
        Melee,
        Rock,
        Water,
        Wind,
        Fire
    }

    
}
