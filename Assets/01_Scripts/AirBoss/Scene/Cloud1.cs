using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud1 : MonoBehaviour
{
    
    [Header("Alerta")]
    public bool beAlert;
    public float alertRange = 14;
    public LayerMask playerLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BeAlert();
    }
    public void BeAlert()
    {
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);
        if (beAlert)
        {
            if (transform.position.y < 70f)
            {
                transform.Translate(0, 10 * Time.deltaTime, 0);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}
