using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyAttackEarthBoss : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] float damage=10;

    [Header("Referencias")]
    [SerializeField] GameObject rockEffect;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(10);
        }
    }

    private void OnDestroy(){
        Instantiate(rockEffect,new Vector3(transform.position.x,transform.position.y + 5,transform.position.z),transform.rotation);
    }
}
