using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class FireBoss : MonoBehaviour,IDamageable
{
    public static float hp;
    float life = 100;

    [Header("Referencias")]
    [SerializeField] GameObject rockEffect;
    public Transform attack2Point;
    public Slider lifebar;
    public GameObject element;

    #region Alert
    bool beAlert;
    public bool found = false;

    public bool Near = false;

    public float alertRange = 10;
    public LayerMask playerLayer;

    public CinemachineTargetGroup cine;
    private GameObject boss;
    
    #endregion

    [Header("Movimiento")]
    public Transform target;
    public float speed = 8;
    bool playerFound = false;

    [Header("Ataques")]
    public GameObject wave;
    public GameObject Attack1Model;
    public Transform A1FirePoint;
    
    [Header("Sonidos")]
    public AudioClip attack1Sound, attack2Sound;

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
        lifebar.value = life;
        Debug.Log("Boss life: " + life);
        if (life <= 0){
            Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
            p.availableAttacks.Add(Player.AttackType.Fire);
            p.availableAttacks.Add(Player.AttackType.Wind);
            p.hp = 100;
            p.heathbar.value = 100;
            lifebar.gameObject.SetActive(false);
            element.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void WaveAttack()
    {
        Quaternion newRotation = Quaternion.Euler(attack2Point.rotation.eulerAngles.x, attack2Point.rotation.eulerAngles.y + 90, attack2Point.rotation.eulerAngles.z);
        Instantiate(wave, new Vector3(attack2Point.position.x, attack2Point.position.y, attack2Point.position.z), newRotation);
        GameManager.instance.PlaySFX(attack2Sound);
    }

    public void AttackOne()
    {
        Instantiate(Attack1Model, new Vector3(A1FirePoint.position.x, A1FirePoint.position.y, A1FirePoint.position.z), Quaternion.Euler(0, 0, 0));
        GameManager.instance.PlaySFX(attack1Sound);
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

