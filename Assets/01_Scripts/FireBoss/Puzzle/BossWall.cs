using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    float puzzleWall = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PuzzleWall(){
        puzzleWall += 1;
        if(puzzleWall >= 2){
            Destroy(gameObject);
            Debug.Log("Pared Destruida");
        }
    }
}
