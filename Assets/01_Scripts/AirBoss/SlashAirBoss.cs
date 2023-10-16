using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAirBoss : MonoBehaviour
{
    float speed = -15;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, (speed * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
