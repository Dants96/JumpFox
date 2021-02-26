using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letreroInfo : MonoBehaviour
{
   public Text textoInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("jugador")) {
            textoInfo.enabled = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        textoInfo.enabled = false;
    }
}
