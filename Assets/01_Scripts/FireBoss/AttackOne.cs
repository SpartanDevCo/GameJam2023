using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
