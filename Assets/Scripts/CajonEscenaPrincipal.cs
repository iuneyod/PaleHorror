using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajonEscenaPrincipal : ObjetoInteractivo
{
    public bool abierto = false;
    public bool puedeInteractuar = true;
    public override void ActivarObjeto()
    {
        if (!puedeInteractuar) return;
        if (abierto == true)
        {
            StartCoroutine(CerrarCajon());
        }
        else
        {
            StartCoroutine(AbrirCajon());
        }
    }

    IEnumerator AbrirCajon()
    {
        puedeInteractuar = false;
        abierto = true;
        float tiempoAnimacion = 0.5f;
        float tiempoTranscurrido = 0f;
        Vector3 posicionInicial = transform.position;
        Vector3 posicionFinal = posicionInicial + new Vector3(0, 0, -0.35f); // Ajusta la distancia que se abre el cajón
        while (tiempoTranscurrido < tiempoAnimacion)
        {
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / tiempoAnimacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        transform.position = posicionFinal;
        puedeInteractuar = true;
    }

    IEnumerator CerrarCajon()
    {
        puedeInteractuar = false;
        abierto = false;
        float tiempoAnimacion = 0.5f;
        float tiempoTranscurrido = 0f;
        Vector3 posicionFinal = transform.position;
        Vector3 posicionInicial = posicionFinal - new Vector3(0, 0, -0.35f); // Ajusta la distancia que se cierra el cajón
        while (tiempoTranscurrido < tiempoAnimacion)
        {
            transform.position = Vector3.Lerp(posicionFinal, posicionInicial, tiempoTranscurrido / tiempoAnimacion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        transform.position = posicionInicial;
        puedeInteractuar = true;
    }
}
