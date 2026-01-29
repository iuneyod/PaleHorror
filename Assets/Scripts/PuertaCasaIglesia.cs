using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCasaIglesia : MonoBehaviour
{
    public bool puertaAbierta = false;
    public bool enArea = false;
    public GameObject puertaCasaIglesia;

     public LLaveIglesia llaveIglesiaScript;

    void Start()
    {

    }

    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && !puertaAbierta && llaveIglesiaScript.tieneLlaveIglesia == true )
        {
            puertaAbierta = true;
            StartCoroutine(AbrirPuertaGranero());
        }
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
    IEnumerator AbrirPuertaGranero()
    {
        float tiempoDuracion = 1f;
        float tiempoTranscurrido = 0f;
        Quaternion rotacionInicialIzq = puertaCasaIglesia.transform.rotation;
        Quaternion rotacionFinalIzq = rotacionInicialIzq * Quaternion.Euler(0f, -90f, 0f);
        while (tiempoTranscurrido < tiempoDuracion)
        {
            float t = tiempoTranscurrido / tiempoDuracion;
            puertaCasaIglesia.transform.rotation = Quaternion.Slerp(rotacionInicialIzq, rotacionFinalIzq, t);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        puertaCasaIglesia.transform.rotation = rotacionFinalIzq;
    }
}
