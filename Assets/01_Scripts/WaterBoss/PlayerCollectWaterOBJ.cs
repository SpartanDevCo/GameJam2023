using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectWaterOBJ : MonoBehaviour
{
    public int totalCubes = 7;
    private int counterCollected = 0;
    public GameObject portal;

    void Start()
    {
        // Make sure the portal is initially invisible
        portal.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterCollectible"))
        {
            CollectCube(other.gameObject);
        }
        else if (other.CompareTag("Portal") && counterCollected == totalCubes)
        {
            EnterPortal();
        }
    }

    void CollectCube(GameObject cube)
    {
        counterCollected++;
        Debug.Log("Collected: " + counterCollected);
        Destroy(cube); // Remove the collected cube from the scene
        if (counterCollected == totalCubes)
        {
            // Activate the portal when all cubes are collected
            portal.SetActive(true);
        }
    }

    void EnterPortal()
    {
        // You can use the teleportation mechanism of your choice.
        // For a simple example, just move the player to the center of the map.
        transform.position = new Vector3(0, 18, 0); // Adjust these coordinates as needed.
    }
}
