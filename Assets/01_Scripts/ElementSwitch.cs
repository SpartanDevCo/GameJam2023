using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Atributos")]
    public int selectedElement = 0; 
    Player p;
    List<Player.AttackType> attacks;

    [Header("Referencias")]
    [SerializeField] Material material;
    [SerializeField] ParticleSystem par;
    void Start()
    {
        p = GetComponent<Player>();
        attacks = Enum.GetValues(typeof(Player.AttackType)).Cast<Player.AttackType>().ToList();
        SkinnedMeshRenderer modelRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        // Comprueba si se encontró el componente
        if (modelRenderer != null)
        {
            // Accede al material del objeto hijo "model"
            material = modelRenderer.material;
        }
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
        var particle = par.main;
        
        switch (p.attackType)
        {
            case Player.AttackType.Rock:
                particle.startColor = new ParticleSystem.MinMaxGradient(new Color(186,191,0));
                ChangeColor(new Color(186,191,0));
                break;
            case Player.AttackType.Wind:
                particle.startColor = new ParticleSystem.MinMaxGradient(new Color(0,255,0));
                ChangeColor(new Color(0,255,0));
                break;
            case Player.AttackType.Water:
                particle.startColor = new ParticleSystem.MinMaxGradient(new Color(0,162,191));
                ChangeColor(new Color(0,162,191));
                break;
            case Player.AttackType.Fire:
                particle.startColor = new ParticleSystem.MinMaxGradient(new Color(255,0,0));
                ChangeColor(new Color(255,0,0));
                break;
            case Player.AttackType.Melee:
                particle.startColor = new ParticleSystem.MinMaxGradient(new Color(255,255,255));
                ChangeColor(new Color(255,255,255));
                break;
        }
        Instantiate(par,transform.position,transform.rotation);
    }

    void ChangeColor(Color color){
        material.SetColor(name:"_GraphColor",color);

    }
}
