using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBall : MonoBehaviour
{
    public float damage=5;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
            other.gameObject.GetComponent<IDamageable>().TakeDamage(7);
        }
    }
}
