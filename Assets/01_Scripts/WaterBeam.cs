using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{
    [SerializeField] float damage=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }else if(other.gameObject.tag == "Boss"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
