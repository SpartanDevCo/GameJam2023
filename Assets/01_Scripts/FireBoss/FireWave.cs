using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWave : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4);
    }

    void Update()
    {
        transform.Translate(Vector3.left * 15 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<IDamageable>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
