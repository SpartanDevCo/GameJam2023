using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    float speed = -15;

    [SerializeField] float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,(speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }else if(other.gameObject.tag == "Boss"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
