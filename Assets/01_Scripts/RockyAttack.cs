using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyAttack : MonoBehaviour
{

    [Header("Referencias")]
    [SerializeField] GameObject rockEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        Instantiate(rockEffect,new Vector3(transform.position.x,transform.position.y + 5,transform.position.z),transform.rotation);   
    }
}
