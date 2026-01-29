using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaGranero : MonoBehaviour
{
    public bool enArea = false;
    public bool puertaAbierta = false;
    public GameObject puertaGraneroizq;
    public GameObject puertaGraneroder;

    AparecerCasaIglesia aparecerCasaIglesia;

    void Start()
    {
        aparecerCasaIglesia = FindObjectOfType<AparecerCasaIglesia>();
    }

    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && !puertaAbierta && aparecerCasaIglesia.tieneLlaveCasaIglesia == true)
        {
            puertaAbierta = true;
            StartCoroutine(AbrirPuertaGranero());
            aparecerCasaIglesia.tieneLlaveCasaIglesia = false;
        }
    }

    IEnumerator AbrirPuertaGranero()
    {
        float tiempoDuracion = 4f;
        float tiempoTranscurrido = 0f;
        Quaternion rotacionInicialIzq = puertaGraneroizq.transform.rotation;
        Quaternion rotacionFinalIzq = rotacionInicialIzq * Quaternion.Euler(0f, -90f, 0f);
        Quaternion rotacionInicialDer = puertaGraneroder.transform.rotation;
        Quaternion rotacionFinalDer = rotacionInicialDer * Quaternion.Euler(0f, 90f, 0f);
        while (tiempoTranscurrido < tiempoDuracion)
        {
            float t = tiempoTranscurrido / tiempoDuracion;
            puertaGraneroizq.transform.rotation = Quaternion.Slerp(rotacionInicialIzq, rotacionFinalIzq, t);
            puertaGraneroder.transform.rotation = Quaternion.Slerp(rotacionInicialDer, rotacionFinalDer, t);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        puertaGraneroizq.transform.rotation = rotacionFinalIzq;
        puertaGraneroder.transform.rotation = rotacionFinalDer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }
}
