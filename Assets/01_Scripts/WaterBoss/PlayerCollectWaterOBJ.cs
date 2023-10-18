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

}
