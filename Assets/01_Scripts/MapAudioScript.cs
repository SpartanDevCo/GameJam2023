using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAudioScript : MonoBehaviour
{
    public AudioClip locationAudio; // Assign the audio clip for this map location in the Inspector.
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component to this GameObject.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change "Player" to the tag of your player GameObject.
        {
            // Play the audio when the player enters the trigger zone.
            if (locationAudio != null)
            {
                audioSource.volume = 0.3f;
                audioSource.clip = locationAudio;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop the audio when the player exits the trigger zone.
            audioSource.Stop();
        }
    }
}

