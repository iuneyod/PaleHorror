using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIglesia : MonoBehaviour
{
    public bool enArea = false;
    public GameObject puertaTrasera;
    public GameObject puertaDelanteraizq;
    public GameObject puertaDelanterader;
    public GameObject EfectoPuertaTrasera;
    public GameObject EfectoPuertaDelanteraIzq;
    public GameObject EfectoPuertaDelanteraDer;

    public GameObject parkourObjeto1;
    public GameObject parkourObjeto2;
    public GameObject parkourObjeto3;
    public GameObject parkourObjeto4;
    public GameObject parkourObjeto5;
    public GameObject parkourObjeto6;
    public GameObject parkourObjeto7;
    public GameObject parkourObjeto8;
    public GameObject parkourObjeto9;
    public GameObject parkourObjeto10;
    public GameObject parkourObjeto11;
    public GameObject parkourObjeto12;
    public GameObject parkourObjeto13;
    public GameObject llaveIglesia;

    public BoxCollider triggerIglesiaCollider;


    LLaveIglesia llaveIglesiaScript;

    public bool tieneLlaveCasaIglesia = false;
    public bool puertaAbierta = false;

    void Start()
    {
        parkourObjeto1.SetActive(false);
        parkourObjeto2.SetActive(false);
        parkourObjeto3.SetActive(false);
        parkourObjeto4.SetActive(false);
        parkourObjeto5.SetActive(false);
        parkourObjeto6.SetActive(false);
        parkourObjeto7.SetActive(false);
        parkourObjeto8.SetActive(false);
        parkourObjeto9.SetActive(false);
        parkourObjeto10.SetActive(false);
        parkourObjeto11.SetActive(false);
        parkourObjeto12.SetActive(false);
        parkourObjeto13.SetActive(false);
        llaveIglesia.SetActive(false);

        llaveIglesiaScript = llaveIglesia.GetComponent<LLaveIglesia>();
    }

    private void Update()
    {
        if (!puertaAbierta && tieneLlaveCasaIglesia == true)
        {
            puertaAbierta = true;
            StartCoroutine(AbrirPuertasIglesia());
            parkourObjeto1.SetActive(false);
            parkourObjeto2.SetActive(false);
            parkourObjeto3.SetActive(false);
            parkourObjeto4.SetActive(false);
            parkourObjeto5.SetActive(false);
            parkourObjeto6.SetActive(false);
            parkourObjeto7.SetActive(false);
            parkourObjeto8.SetActive(false);
            parkourObjeto9.SetActive(false);
            parkourObjeto10.SetActive(false);
            parkourObjeto11.SetActive(false);
            parkourObjeto12.SetActive(false);
            parkourObjeto13.SetActive(false);
            llaveIglesia.SetActive(false);
        }

        if (llaveIglesiaScript.tieneLlaveIglesia)
        {
            tieneLlaveCasaIglesia = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerIglesiaCollider.enabled = false;
            enArea = true;
            StartCoroutine(CerrarPuertasIglesia());
            StartCoroutine(StartParkour());
        }
    }

    IEnumerator StartParkour()
    {
        yield return new WaitForSeconds(3f);
        parkourObjeto1.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        parkourObjeto2.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        parkourObjeto3.SetActive(true);
        yield return new WaitForSeconds(1f);
        parkourObjeto4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        parkourObjeto5.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto6.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto7.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto8.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto9.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto10.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto11.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto12.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        parkourObjeto13.SetActive(true);
        yield return new WaitForSeconds(1f);
        llaveIglesia.SetActive(true);
    }

    IEnumerator CerrarPuertasIglesia()
    {
        float tiempoDuracion = 1f;
        float tiempoTranscurrido = 0f;
        Quaternion rotacionInicialIzq = puertaDelanteraizq.transform.rotation;
        Quaternion rotacionIniciaTras = puertaTrasera.transform.rotation;
        Quaternion rotacionFinalIzq = rotacionInicialIzq * Quaternion.Euler(0f, +75f, 0f);
        Quaternion rotacionFinalTras = rotacionIniciaTras * Quaternion.Euler(0f, +72.585f, 0f);
        Quaternion rotacionInicialDer = puertaDelanterader.transform.rotation;
        Quaternion rotacionFinalDer = rotacionInicialDer * Quaternion.Euler(0f, -60f, 0f);
        while (tiempoTranscurrido < tiempoDuracion)
        {
            float t = tiempoTranscurrido / tiempoDuracion;
            puertaDelanteraizq.transform.rotation = Quaternion.Slerp(rotacionInicialIzq, rotacionFinalIzq, t);
            puertaDelanterader.transform.rotation = Quaternion.Slerp(rotacionInicialDer, rotacionFinalDer, t);
            puertaTrasera.transform.rotation = Quaternion.Slerp(rotacionIniciaTras, rotacionFinalTras, t);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        puertaDelanteraizq.transform.rotation = rotacionFinalIzq;
        puertaDelanterader.transform.rotation = rotacionFinalDer;
        puertaTrasera.transform.rotation = rotacionFinalTras;
    }

    IEnumerator AbrirPuertasIglesia()
    {
        float tiempoDuracion = 3f;
        float tiempoTranscurrido = 0f;
        Quaternion rotacionInicialIzq = puertaDelanteraizq.transform.rotation;
        Quaternion rotacionIniciaTras = puertaTrasera.transform.rotation;
        Quaternion rotacionFinalIzq = rotacionInicialIzq * Quaternion.Euler(0f, +75f, 0f);
        Quaternion rotacionFinalTras = rotacionIniciaTras * Quaternion.Euler(0f, -72.585f, 0f);
        Quaternion rotacionInicialDer = puertaDelanterader.transform.rotation;
        Quaternion rotacionFinalDer = rotacionInicialDer * Quaternion.Euler(0f, +60f, 0f);
        while (tiempoTranscurrido < tiempoDuracion)
        {
            float t = tiempoTranscurrido / tiempoDuracion;
            puertaDelanteraizq.transform.rotation = Quaternion.Slerp(rotacionInicialIzq, rotacionFinalIzq, t);
            puertaDelanterader.transform.rotation = Quaternion.Slerp(rotacionInicialDer, rotacionFinalDer, t);
            puertaTrasera.transform.rotation = Quaternion.Slerp(rotacionIniciaTras, rotacionFinalTras, t);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        puertaDelanteraizq.transform.rotation = rotacionFinalIzq;
        puertaDelanterader.transform.rotation = rotacionFinalDer;
        puertaTrasera.transform.rotation = rotacionFinalTras;
    }
}

