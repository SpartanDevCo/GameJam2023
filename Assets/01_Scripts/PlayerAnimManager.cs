using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{

    public Player p;
    public ComboSystem comb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VerifyCombo(){
        comb.VerifyCombo();
    }

    public void SpawnClaws(float deg){
        comb.SpawnClaws(deg);
    }

    public void Attack(){
        p.Attack();
    }

    public void ReturnNormal(){
        p.ReturnToNormal();
    }

    public void ReturnCinematicNormal(){
        p.ReturnCinematicNormal();
    }
}
