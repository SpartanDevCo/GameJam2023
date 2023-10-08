using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] float speed = 7;
    [SerializeField] float jumpSpeed = 7;
    float distanceToGround;

    [Header("Referencias")]
    [SerializeField] Rigidbody rb;
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
            Debug.Log("SALTO");
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }
}
