using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalWater : MonoBehaviour
{
    public float radio = 1f; // Radio de la esfera
    public LayerMask playerLayer; // Capa del jugador
    public Transform spawnpoint;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radio); // Dibujar la esfera con Gizmos

        // Verificar si hay algo en la capa del jugador dentro de la esfera
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio, playerLayer);
        if (colliders.Length > 0)
        {
            // Si hay algo en la capa del jugador, obtener el Transform del primer objeto encontrado
            colliders[0].transform.position = spawnpoint.position;
        }
    }
}
