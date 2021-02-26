using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SapoScript : MonoBehaviour
{
    public float velocidad = 1;
    private bool ladoDer;
    public Animator animator; // acceder a las animaciones 
    public const float offsetY = 0.4f, offsetX = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("velocidad", Mathf.Abs(velocidad));
    }

    // Update is called once per frame
    void Update()
    {
        // sim mira hacia la derecha
        if (ladoDer)
        {
            transform.Translate(2 * Time.deltaTime * velocidad, 0, 0); // mover objeto hacia la derecha
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * velocidad, 0, 0); // mover objeto hacia la izq
        }
    }

    // coliciones
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("columna") || collision.gameObject.CompareTag("enemigo")) {

            Girar();
        }else if (collision.gameObject.CompareTag("jugador"))
        {          
            if (transform.position.y + 0.45f < collision.transform.position.y && collision.transform.position.x < transform.position.x + offsetX && collision.transform.position.x > transform.position.x - offsetX)
            {
                velocidad = 0;
                animator.SetBool("estaAplastado", true);
                Invoke("Morir", 1); // invocar la funcion despues de 1s
            }
        }
    }

    private void Girar()
    {
        ladoDer = !ladoDer;
        Vector2 escala = gameObject.transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void Morir()
    {
        Destroy(this.gameObject);
    }
}
