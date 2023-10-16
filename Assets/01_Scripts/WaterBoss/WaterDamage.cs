using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyPlayer(other.gameObject);
        }
    }

    private void DestroyPlayer(GameObject player)
    {
        
        Destroy(player);
    }
}
