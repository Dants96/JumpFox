using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class foxScript : MonoBehaviour
{
   
    private Rigidbody2D rb; // fisicas 2d del jugador
    public float velocidad = 3.5f; // vlocidad estandar
    private float velocidadCal; // velocidad calculada 
    public float drift = 0.085f; // dirft, rozamiento al estar agachado
    private float driftRun = 0.05f; //drift mientras corre;
    public float aumentoVelocidad = 2;
    private bool supVelAct = false;  // aumento de velocidad activado?
    private bool ladoDer = true; // esta de lado derecho ?
    private float horizontalMov;    // movimiento horizontal getAxis
    public Animator animator; // objeto de animaciones 
    public float frSalto = 2.5f; // fuerza de salto

    // varianbles para salto 
    [HideInInspector]
    public bool estaPisando; // esta pisando superficies
    public Transform piesPos; // colider de los pies
    public float checkRadius = 0.1f;  // valor de comparacion de radio 
    public LayerMask quePisa; // saber que layaout esta pisando el zorrito
    [SerializeField] private float ContTimeSalto; //tiempo de presion de la tecla de salto
    public float timeSalto = 0.4f; //tiempo de salto estandar
    private bool estaSaltando; // esta saltando?

    // ariables para agacharse
    private bool estaAgachado = false; // esta agachado?

    [HideInInspector]
    public bool usandoEscaleras = false; //esta en las escalleras?

    // variables de hub
    public HUBmanager _hubManager;
    private int vidas;

    // variables para muerte
    private bool estaMuerto;
   


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadCal = velocidad;
        vidas = 3;
        _hubManager = GameObject.Find("HUB").GetComponent<HUBmanager>();
        
        if (_hubManager != null)
        {
            _hubManager.MostrarVidas(vidas);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!estaMuerto)
        {
            //comprobar si esta sobre el suelo
            estaPisando = Physics2D.OverlapCircle(piesPos.position, checkRadius, quePisa);
            animator.SetBool("estaPisando", estaPisando);

            if (!usandoEscaleras)
            {
                animator.SetFloat("verVelocidad", rb.velocity.y);
            }

            // el personage giro ?
            if (horizontalMov < 0.0f && ladoDer)
            {
                Girar();
            }
            else if (horizontalMov > 0.0f && !ladoDer)
            {
                Girar();
            }

            // saltar con espacio o flecha
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && estaPisando))
            {
                Saltar();
            }
            // salto por precion de teclas
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && estaSaltando)
            {

                if (ContTimeSalto > 0)
                {
                    float frSaltoC = estaAgachado ? frSalto * 1.5f : frSalto; // clausula si hace super salto
                    rb.velocity = Vector2.up * frSaltoC;
                    ContTimeSalto -= Time.deltaTime;
                }
                else
                {
                    estaSaltando = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                estaSaltando = false;
            }

            // super velociodad shift
            if (Input.GetKey(KeyCode.LeftShift) && !supVelAct)
            {
                velocidad += aumentoVelocidad;
                supVelAct = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                velocidad -= aumentoVelocidad;
                supVelAct = false;
            }

            // agacharse
            if (Input.GetKey(KeyCode.DownArrow) && !estaAgachado)
            {

                estaAgachado = true;
                animator.SetBool("estaAgachado", estaAgachado);

            }
            if (Input.GetKeyUp(KeyCode.DownArrow) && estaAgachado)
            {
                estaAgachado = false;
                animator.SetBool("estaAgachado", estaAgachado);
            }
        }
        else if (transform.position.y < -8)
        {
            SceneManager.LoadScene(0);
            //Destroy(this.gameObject);
               
        }
        
    }

    // UPDATE independiente de el framrate, mejor para calculos fisicos 
    private void FixedUpdate()
    {
        if (!estaMuerto)
        {
            horizontalMov = Input.GetAxis("Horizontal"); // capturar teclas de movimiento y AD
            animator.SetFloat("velocidad", Mathf.Abs(horizontalMov)); //Mathf.abs para absolutos flotantes

            // calcula velocidad en diferentes estado, agachado corriendo, etc.
            velocidadCal = estaAgachado ? velocidadCal - velocidadCal * (supVelAct ? driftRun : drift) : velocidad;
            rb.velocity = new Vector2(horizontalMov * velocidadCal, rb.velocity.y);
        }
        
    }


    // girar personage al lado contrario
    private void Girar()
    {
        ladoDer = !ladoDer;
        Vector2 escala = gameObject.transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

    }

    // saltar y saltar con supersalo
    private void Saltar()
    {

        float frSaltoC = estaAgachado ? frSalto * 1.5f : frSalto; // clausula si hace super salto
        rb.velocity = Vector2.up * frSaltoC;
        estaSaltando = true;
        ContTimeSalto = timeSalto;

    } 

    public void QuitarVida()
    {
        vidas -= 1;
        if (vidas > 0)
        {
            _hubManager.MostrarVidas(vidas);            
        }else if(vidas == 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        estaMuerto = true;
        _hubManager.MostrarVidas(0); 
        animator.SetBool("estaMuerto", estaMuerto);
        // saltar despues de morir 
        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        rb.velocity = Vector2.up * frSalto * 2.5f;
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("MainCamera").GetComponent<Camera>().GetComponent<CinemachineBrain>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("vida") && vidas < 3) {           
            vidas++;
            _hubManager.MostrarVidas(vidas);
            _hubManager.SumarPuntos(200);
            collision.GetComponent<vidaItem>().Cojer();
            
        }
        else if(collision.gameObject.CompareTag("vida")) {
            _hubManager.MostrarVidas(vidas);
            _hubManager.SumarPuntos(500);
            collision.GetComponent<vidaItem>().Cojer();
  
        }  
    }
}
