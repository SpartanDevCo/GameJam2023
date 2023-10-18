using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementText : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ShowText()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
