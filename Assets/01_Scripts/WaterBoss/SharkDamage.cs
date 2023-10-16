using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDamage : MonoBehaviour,IDamageable
{
    public float maxHealth = 100;
    private float currentHealth;
    public GameObject finalObject;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RockAttack") || other.CompareTag("AirSlash"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        // Implement death behavior here, like playing an animation, destroying the shark object, or triggering any other actions.
        Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
        p.availableAttacks.Add(Player.AttackType.Water);
        Destroy(gameObject);
        finalObject.SetActive(true);
        
    }
}
