using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyAttack : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] float damage=2;

    [Header("Referencias")]
    [SerializeField] GameObject rockEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
        }else if(other.gameObject.tag == "Boss"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }


    private void OnDestroy() {
        Instantiate(rockEffect,new Vector3(transform.position.x,transform.position.y + 5,transform.position.z),transform.rotation);   
    }
}
