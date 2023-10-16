using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] float damage = 1;
    [SerializeField] float multiplier=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage*multiplier);
        }else if(other.gameObject.tag == "Boss"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
        if (other.gameObject.tag == "Pilar")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage*multiplier);
        }
    }
}
