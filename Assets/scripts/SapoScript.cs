using UnityEngine;

public class SapoScript : AnimalEnemigo
{
    private const int puntosRata = 200;

    protected override void CallinStar()
    {
        this.animator.SetFloat("velocidad", Mathf.Abs(this.velocidad));
        this.PUNTOS = puntosRata;

    }
}
