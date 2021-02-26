using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class casaScript : MonoBehaviour
{
    public Text textoInfo;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("jugador")) {
            textoInfo.enabled = true;
            Invoke("MostrarCreditos", 2);
            

        }
    }

    private void MostrarCreditos(){
        SceneManager.LoadScene(2);
    }
}
