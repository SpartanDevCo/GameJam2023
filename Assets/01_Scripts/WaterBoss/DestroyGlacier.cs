using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGlacier : MonoBehaviour
{
    public GameObject destructionEffect; // Optional: A particle system or other effect for destruction

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AirSlash"))
        {
            DestroyGlacierOBJ();
        }
    }

    private void DestroyGlacierOBJ()
    {
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
