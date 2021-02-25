using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public foxScript fox;

    public BoxCollider2D suleoPlataforma;
    public PlatformEffector2D sueloEfector;
    private bool enEscaleras;
    public float velocidadEsc;
    public float saltoEsc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fox = GetComponent<foxScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("escaleras"))
        {
                      
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * velocidadEsc);
                rb.gravityScale = 0;
                enEscaleras = true;
                suleoPlataforma.enabled = false;
                sueloEfector.surfaceArc = 0;
                fox.usandoEscaleras = enEscaleras;
            }
            else if(Input.GetAxisRaw("Vertical") == 0 && enEscaleras)
            {

                rb.velocity = new Vector2(rb.velocity.x, 0);

            }

            animator.SetBool("enEscaleras", enEscaleras);
            animator.SetFloat("verVelocidad", Mathf.Abs(Input.GetAxisRaw("Vertical")));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("escaleras") && enEscaleras)
        {
            rb.gravityScale = 1;
            enEscaleras = false;
            fox.usandoEscaleras = enEscaleras;
            suleoPlataforma.enabled = true;
            sueloEfector.surfaceArc = 180;
            animator.SetBool("enEscaleras", enEscaleras);

            if (!fox.estaPisando)
            {
                rb.velocity = new Vector2(rb.velocity.x, saltoEsc);
            }
        }
    }
}
