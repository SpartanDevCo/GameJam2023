using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollectWaterOBJ : MonoBehaviour
{
    public int totalCubes = 7;
    private int counterCollected = 0;
    public GameObject portal;
    public TMP_Text textCount;
    public GameObject countImg;

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
        textCount.gameObject.SetActive(true);
        countImg.SetActive(true);
        counterCollected++;
        textCount.text = counterCollected.ToString() + "/10";
        Debug.Log("Collected: " + counterCollected);
        Destroy(cube); // Remove the collected cube from the scene
        if (counterCollected == totalCubes)
        {
            // Activate the portal when all cubes are collected
            portal.SetActive(true);
            textCount.gameObject.SetActive(false);
            countImg.SetActive(false);
        }
    }

    void EnterPortal()
    {
        // You can use the teleportation mechanism of your choice.
        // For a simple example, just move the player to the center of the map.
        transform.position = new Vector3(0, 18, 0); // Adjust these coordinates as needed.
    }
}
