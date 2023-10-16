using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource musicAS;
    public AudioClip[] musicClips; // Array de clips de música, uno para cada escenario

    void Start()
    {
        musicAS.loop = true; // Configurar el AudioSource para que repita la música
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Llamar a este método cuando el jugador entre en un nuevo escenario
    public void ChangeMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            Debug.Log("Cambiando música a " + musicClips[index].name);
            musicAS.clip = musicClips[index];
            musicAS.Play();
        }
    }
}
