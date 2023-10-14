using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarEnergy : MonoBehaviour, IDamageable
{
    [Header("Atributos")]
    public float hp = 5;
    [Header("Referencias")]
    public Rigidbody rb;
    public GameObject[] boxes;
    // Start is called before the first frame update
    void Start()
    {
        // // Desactivar la gravedad de las cajas al inicio
        // foreach (GameObject box in boxes)
        // {
        //     Rigidbody boxRb = box.GetComponent<Rigidbody>();
        //     if (boxRb != null)
        //     {
        //         boxRb.useGravity = false;
        //     }
        // }

        // Desactivar la gravedad de las cajas al inicio
        ManipulationGravity(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ManipulationGravity(bool response)
    {
        foreach (GameObject box in boxes)
        {
            Rigidbody boxRb = box.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.useGravity = response;
            }
        }
    }

    public void TakeDamage(float damage){
        hp -= damage;
        Debug.Log("Pilar hp: " + hp);
        if (hp <= 0){
            // Activar la gravedad de las cajas
            ManipulationGravity(true);

            Destroy(gameObject);
        }
    }
}
