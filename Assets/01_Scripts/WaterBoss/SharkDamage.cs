using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkDamage : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
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

    public void TakeDamage(int damage)
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
        Destroy(gameObject);
        finalObject.SetActive(true);
        
    }
}
