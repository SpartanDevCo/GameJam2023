using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : MonoBehaviour
{
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
            other.gameObject.GetComponent<IDamageable>().TakeDamage(1);
        }
        if (other.gameObject.tag == "Pilar")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(1);
        }
    }
}
