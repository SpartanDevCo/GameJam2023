using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementSwitch : MonoBehaviour
{
    // Start is called before the first frame update

    public int selectedElement = 0; 
    Player p;
    List<Player.AttackType> attacks;
    void Start()
    {
        p = GetComponent<Player>();
        attacks = Enum.GetValues(typeof(Player.AttackType)).Cast<Player.AttackType>().ToList();
        SelectElement();
    }

    // Update is called once per frame
    void Update()
    {
        int previusElement = selectedElement;
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            if(selectedElement >= attacks.Count-1){
                selectedElement = 0;
            }
            else{
                selectedElement++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            if(selectedElement <= 0){
                selectedElement = attacks.Count -1;
            }
            else{
                selectedElement--;
            }
        }

        if(previusElement != selectedElement){
            SelectElement();
        }
    }

    void SelectElement(){
        p.attackType = attacks[selectedElement];
    }
}
