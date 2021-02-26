using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalEnemigo : MonoBehaviour
{
    public float velocidad = 0;
    private bool ladoDer;
    public Animator animator; // acceder a las animaciones 
    private const float offsetY = 0.4f, offsetX = 0.6f;
    protected int PUNTOS;
    public float saltoEne = 3; 

    public GameObject jugador;

    // variables de hub
    public HUBmanager _hubManager;

    protected abstract void CallinStar();

    void Start()
    {
        _hubManager = GameObject.Find("HUB").GetComponent<HUBmanager>();
        jugador = GameObject.FindGameObjectWithTag("jugador");
        CallinStar();

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
        if (collision.gameObject.CompareTag("columna") || collision.gameObject.CompareTag("enemigo"))
        {

            Girar();
        }
        else if (collision.gameObject.CompareTag("jugador"))
        {
            if (transform.position.y + offsetY < collision.transform.position.y && collision.transform.position.x < transform.position.x + offsetX && collision.transform.position.x > transform.position.x - offsetX)
            {
                velocidad = 0;
                jugador.GetComponent<Rigidbody2D>().velocity = Vector2.up * saltoEne;
                animator.SetBool("estaAplastado", true);
                if (_hubManager != null) _hubManager.SumarPuntos(PUNTOS);
                GetComponentInChildren<CircleCollider2D>().enabled = false;
                Invoke("Morir", 1); // invocar la funcion despues de 1s
            }
            else
            {
                jugador.GetComponent<foxScript>().QuitarVida();
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
