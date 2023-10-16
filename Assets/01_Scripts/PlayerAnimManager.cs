using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void Lose(){
        SceneManager.LoadScene("LosingScene");
    }

    public void ChangeAnimInProgress(int setAnim){
        if(setAnim ==1)
            p.ChangeAnimInProgress(true);
        else if(setAnim == 0)
            p.ChangeAnimInProgress(false);
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
