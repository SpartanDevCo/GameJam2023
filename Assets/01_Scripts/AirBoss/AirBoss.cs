using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirBoss : MonoBehaviour
{
    [Header("Atributos")]
    public float minWaitTime = 7;
    public float maxWaitTime = 10;
    public int life = 100;
    public Animator anim;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Attack 1
    public void LookAtPlayer()
    {
        targetPosition = target.position;
        Vector3 dir = targetPosition - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }

    public void Shoot()
    {
        float angleX = 0;
        targetPosition = target.position;
        Vector3 dir = targetPosition - transform.position;
        angleX = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg + 180;
        Instantiate(bulletPrefab, firepoint.position,
            Quaternion.Euler( -angleX, firepoint.rotation.y, firepoint.rotation.z));
        Debug.Log(angleX);
    }
    #endregion

    #region Attack 2

    public void Tornado()
    {
        Instantiate(tornadoEffect, new Vector3(tornadoSpawnPoint.position.x, tornadoSpawnPoint.position.y - 6f, tornadoSpawnPoint.position.z), tornadoSpawnPoint.rotation);
    }
    #endregion
}
