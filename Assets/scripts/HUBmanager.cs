using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUBmanager : MonoBehaviour
{

    public Sprite[] vidasImgs;
    public Text puntosDisplay;
    public Image MuestraVidas;
    [SerializeField] private int puntos;
    // Start is called before the first frame update
    void Start()
    {
        puntos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MostrarPuntos();
    }

    private void MostrarPuntos()
    {
        puntosDisplay.text = puntos.ToString();
    }

    public void MostrarVidas(int nVidas)
    {
        MuestraVidas.sprite = vidasImgs[nVidas];
    }

    public void SumarPuntos(int puntos)
    {
        this.puntos = puntos;
    }
}
