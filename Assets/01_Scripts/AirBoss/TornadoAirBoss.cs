using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TornadoAirBoss : MonoBehaviour
{
    // Start is called before the first frame update
    bool damage;
    int speed = 5;
    Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 10);
        damage = true;
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        Move();
    }
    void LookAt()
    {
        Vector3 dir = target.position - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.Euler(-90, angleY, 0);
    }
    void Move()
    {
        transform.Translate(0, (speed * Time.deltaTime), 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && damage)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(20);
            Destroy(gameObject);
            damage = false;
        }
    }
}
