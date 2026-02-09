using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMinigame4 : ObjetoInteractivo
{
    public bool mirandoFrente = false;
    public bool mirandoAtras = false;
    public bool mirandoIzquierda = false;
    public bool mirandoDerecha = true;

    private bool puedeGirar = true;
    public AudioSource eagle;


    public override void ActivarObjeto()
    {
        StartCoroutine(Girar());
    }

    private void Update()
    {
        if (transform.rotation.eulerAngles.y >= 315 || transform.rotation.eulerAngles.y < 45)
        {
            mirandoDerecha = true;
            mirandoFrente = false;
            mirandoAtras = false;
            mirandoIzquierda = false;
        }
        else if (transform.rotation.eulerAngles.y >= 45 && transform.rotation.eulerAngles.y < 135)
        {
            mirandoFrente = true;
            mirandoDerecha = false;
            mirandoAtras = false;
            mirandoIzquierda = false;
        }
        else if (transform.rotation.eulerAngles.y >= 135 && transform.rotation.eulerAngles.y < 225)
        {
            mirandoIzquierda = true;
            mirandoFrente = false;
            mirandoAtras = false;
            mirandoDerecha = false;
        }
        else if (transform.rotation.eulerAngles.y >= 225 && transform.rotation.eulerAngles.y < 315)
        {
            mirandoAtras = true;
            mirandoFrente = false;
            mirandoIzquierda = false;
            mirandoDerecha = false;
        }
    }

    IEnumerator Girar()
    {
        if(puedeGirar == true)
        {
            eagle.Play();
            puedeGirar = false;
            float tiempoTranscurrido = 0f;
            float duracionGiro = 0.5f; // Duración del giro en segundos
            Quaternion rotacionInicial = transform.rotation;
            Quaternion rotacionFinal = rotacionInicial * Quaternion.Euler(0, 90, 0);
            while (tiempoTranscurrido < duracionGiro)
            {
                transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, tiempoTranscurrido / duracionGiro);
                tiempoTranscurrido += Time.deltaTime;
                yield return null;
            }
            puedeGirar = true;
        }
    }
}
