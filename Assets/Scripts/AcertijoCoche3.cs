using System.Collections;
using UnityEngine;

public class AcertijoCoche3 : ObjetoInteractivo
{
    public Transform coche3;
    CogerCoche cogerCoche;

    public float duracionGiro = 0.5f;
    private bool girando = false;

    private int direccionActual = 0;
    // 0 = frente, 1 = derecha, 2 = atrás, 3 = izquierda


    public bool mirandoFrente;
    public bool mirandoDerecha;
    public bool mirandoAtras;
    public bool mirandoIzquierda;
    public AudioSource rotar;


    void Start()
    {
        cogerCoche = coche3.GetComponent<CogerCoche>();
        ActualizarDireccion();
    }

    void ActualizarDireccion()
    {
        mirandoFrente = mirandoDerecha = mirandoAtras = mirandoIzquierda = false;

        float y = Mathf.Round(coche3.eulerAngles.y);

        if (y == 0 || y == 360) mirandoFrente = true;
        else if (y == 90) mirandoDerecha = true;
        else if (y == 180) mirandoAtras = true;
        else if (y == 270) mirandoIzquierda = true;
    }


    private Quaternion[] rotaciones =
    {
        Quaternion.Euler(0, 0, 0),     // frente
        Quaternion.Euler(0, 90, 0),    // derecha
        Quaternion.Euler(0, 180, 0),   // atrás
        Quaternion.Euler(0, 270, 0)    // izquierda
    };

    public override void ActivarObjeto()
    {
        if(cogerCoche.ObjetoColocado == false)
            return;
        if (!girando)
            StartCoroutine(Girar());
        rotar.Play();

    }

    IEnumerator Girar()
    {
        girando = true;

        Quaternion inicio = coche3.rotation;
        direccionActual = (direccionActual + 1) % rotaciones.Length;
        Quaternion destino = rotaciones[direccionActual];

        float t = 0f;
        while (t < duracionGiro)
        {
            coche3.rotation = Quaternion.Slerp(inicio, destino, t / duracionGiro);
            t += Time.deltaTime;
            yield return null;
        }

        coche3.rotation = destino;
        ActualizarDireccion();
        girando = false;

    }
}
