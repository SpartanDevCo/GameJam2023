using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotemText : MonoBehaviour
{

    [Header("Alerta")]
    public bool beAlert;
    public float alertRange = 12;
    public LayerMask playerLayer;
    public TMP_Text TextUi;
    public GameObject img;
    public string text;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        BeAlert();
    }
    public void BeAlert()
    {
        //Debug.Log("1");
        beAlert = Physics.CheckSphere(transform.position, alertRange, playerLayer);
        if (beAlert)
        {
         
            TextUi.gameObject.SetActive(true);
            img.SetActive(true);
            TextUi.text = text;
        }
        else
        {
            TextUi.gameObject.SetActive(false);
            img.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}
