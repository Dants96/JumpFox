using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jugador"))
        {
            Invoke("Matar", 0.2f);
        }
    }

    private void Matar()
    {
       GameObject.FindGameObjectWithTag("jugador").GetComponent<foxScript>().QuitarVida();
    }

}
