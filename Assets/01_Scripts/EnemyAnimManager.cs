using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimManager : MonoBehaviour
{
    public float damage = 1;
    public float attackRange=5;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(){
        Collider[] players = Physics.OverlapSphere(transform.position,attackRange,playerLayer);
        foreach(Collider player in players){
            player.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
