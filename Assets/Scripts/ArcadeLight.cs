using UnityEngine;

public class ArcadeLight : MonoBehaviour
{
    public bool activa = false;
    SpriteRenderer sr;
    public bool fuePulsada { get; private set; }


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            Debug.LogError("ArcadeLight necesita un SpriteRenderer", this);
        }

        Apagar();
    }

    public void Encender()
    {
        activa = true;
        fuePulsada = false;
        sr.color = Color.white;
    }

    public void Apagar()
    {
        activa = false;
        sr.color = Color.gray;
    }

    public void Pulsar()
    {
        ArcadeGame game = FindObjectOfType<ArcadeGame>();

        if (!activa) return;

        fuePulsada = true;
        activa = false;
        sr.color = Color.gray;

        game.Acierto();
    }

    void OnMouseDown()
    {
        Pulsar();
    }
}
