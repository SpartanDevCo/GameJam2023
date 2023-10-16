using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicTrigger : MonoBehaviour
{
    public BackgroundMusic bgMusic;
    public int musicIndex;
    bool playingMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (!playingMusic)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                bgMusic.ChangeMusic(musicIndex);
                playingMusic = true;
            }
        }
    }
}
