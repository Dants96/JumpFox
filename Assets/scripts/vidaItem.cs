using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaItem : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cojer()
    {
        animator.SetTrigger("cojer");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("Destruir", 0.5f);
    }

    private void Destruir()
    {
        Destroy(this.gameObject);
    }
}
