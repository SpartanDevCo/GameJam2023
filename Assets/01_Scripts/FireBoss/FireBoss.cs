using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class FireBoss : MonoBehaviour,IDamageable
{
    float life = 100;
    public Transform attack2Point;
    [Header("Referencias")]
    [SerializeField] GameObject rockEffect;
    public static float hp;
    public Slider lifebar;

    #region Alert
    bool beAlert;
    public bool found = false;

    public bool Near = false;

    public float alertRange = 10;
    public LayerMask playerLayer;

    public CinemachineTargetGroup cine;
    private GameObject boss;
    
    #endregion

    #region Movement
    public Transform target;
    public float speed = 8;
    bool playerFound = false;

    #endregion

    #region Attack 2
    public GameObject wave;
    #endregion

    #region Movement
    public GameObject Attack1Model;
    public Transform A1FirePoint;
    #endregion

    void Start()
    {
        lifebar.maxValue = life;
        lifebar.value = life;
    }

    void Update()
    {
        BeAlert();
        CheckDistanceAndAttack();

    }

    public void TakeDamage(float damage){
        life -= damage;
        Debug.Log("Boss life: " + life);
        if (life <= 0){
            Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
            p.availableAttacks.Add(Player.AttackType.Fire);
            p.availableAttacks.Add(Player.AttackType.Wind);
            p.hp = 100;
            p.heathbar.value = 100;
            lifebar.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void WaveAttack()
    {
        Quaternion newRotation = Quaternion.Euler(attack2Point.rotation.eulerAngles.x, attack2Point.rotation.eulerAngles.y + 90, attack2Point.rotation.eulerAngles.z);
        Instantiate(wave, new Vector3(attack2Point.position.x, attack2Point.position.y, attack2Point.position.z), newRotation);
    }

    public void AttackOne()
    {
        Instantiate(Attack1Model, new Vector3(A1FirePoint.position.x, A1FirePoint.position.y, A1FirePoint.position.z), Quaternion.Euler(0, 0, 0));
    }
    public void BeAlert()
    {
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);

        if (beAlert == true)
        {
            if (!found)
            {
                boss = GameObject.FindGameObjectWithTag("Boss");
                //cine.AddMember(boss.transform, 1, 1);
                found = true;
            }
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            playerFound = true;
            lifebar.gameObject.SetActive(true);
        }
    }
    public void CheckDistanceAndAttack()
    {
        if (Vector3.Distance(transform.position, target.position) <= 10)
        {
            Near = true;
        }
        else
        {
            Near = false;
        }
    }
    public void InstantiateRockEffect()
    {
        Instantiate(rockEffect, new Vector3(attack2Point.position.x, attack2Point.position.y + 5, attack2Point.position.z), attack2Point.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}

