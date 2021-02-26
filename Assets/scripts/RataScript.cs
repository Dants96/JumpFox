

public class RataScript : AnimalEnemigo
{
    private const int puntosRata = 100;

    protected override void CallinStar()
    {
        this.PUNTOS = puntosRata;

    }
}
