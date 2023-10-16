using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirBoss : MonoBehaviour, IDamageable
{
    [Header("Atributos")]
    public float minWaitTime = 7;
    public float maxWaitTime = 10;
    public float life = 100;
    public Animator anim;
    public Slider lifebar;
    public bool death;

    [Header("Alerta")]
    public bool beAlert;
    public float alertRange = 60;
    public LayerMask playerLayer;
  

    [Header("Attack 1")]
    public Transform target;
    public Vector3 targetPosition;
    public float timeBtwShoot = 7f;
    public int bulletCount = 5;
    public GameObject bulletPrefab;
    public Transform firepoint;

    [Header("Attack 2")]
    public List<TargetInfo> PatrolPoints;
    [SerializeField] Transform tornadoSpawnPoint;
    [SerializeField] GameObject tornadoEffect;
    public 


    void Start()
    {
        lifebar.maxValue = 100;
        lifebar.value = 100;
        death = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Attack 1
    public void LookAtPlayer()
    {
        // targetPosition = target.position;
        // Vector3 dir = targetPosition - transform.position;
        // float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 180;
        // transform.rotation = Quaternion.Euler(0, angleY, 0);
        transform.LookAt(target);
    }

    public void Shoot()
    {  
        
        // float angleX = 0;
        // targetPosition = target.position;
        // Vector3 dir = targetPosition - transform.position;
        // angleX = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg + 180;
        // Instantiate(bulletPrefab, firepoint.position,
        //     Quaternion.Euler( -angleX, firepoint.rotation.y, firepoint.rotation.z));
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

    }
    #endregion

    #region Attack 2

    public void Tornado()
    {
        Instantiate(tornadoEffect, new Vector3(tornadoSpawnPoint.position.x, tornadoSpawnPoint.position.y - 6f, tornadoSpawnPoint.position.z), tornadoSpawnPoint.rotation);
    }
    #endregion


    public void BeAlert()
    {
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);
    }
     private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }

    public void TakeDamage(float damage)
    {
        if(life > 0)
        {
            life -= damage;
        }
        else
        {
            death= true;
            Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
            p.availableAttacks.Add(Player.AttackType.Wind);
        }
    }
}
