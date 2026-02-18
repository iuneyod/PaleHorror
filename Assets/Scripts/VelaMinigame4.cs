using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelaMinigame4 : ObjetoInteractivo
{
    Vela2MInigame4 Vela2MInigame4;
    Vela3Minigame4 Vela3Minigame4;
    Vela4Minigame4 Vela4Minigame4;

    ObjetoMinigame4NoApagarVela objetoNoApagarVela;
    public GameObject fire;
    public bool velaEncendida = false;
    public bool puedeEncenderla = false;

    public bool accesoPermitido = false;
    public GameObject puertaAcceso;

    public AudioSource abrirPuerta;

    public AudioSource vela;

    private void Start()
    {
        fire.SetActive(false);
        objetoNoApagarVela = FindObjectOfType<ObjetoMinigame4NoApagarVela>();
        Vela2MInigame4 = FindObjectOfType<Vela2MInigame4>();
        Vela3Minigame4 = FindObjectOfType<Vela3Minigame4>();
        Vela4Minigame4 = FindObjectOfType<Vela4Minigame4>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void ActivarObjeto()
    {
        if (puedeEncenderla && !velaEncendida)
        {
            vela.Play();
            fire.SetActive(true);
            velaEncendida = true;
        }

        if(objetoNoApagarVela.agarrandoObjeto == true)
        {
            objetoNoApagarVela.colocarObjeto = true;
        }
    }

    void Update()
    {
        if (velaEncendida == true && Vela2MInigame4.vela2Encendida && Vela3Minigame4.vela3Encendida && Vela4Minigame4.vela4Encendida)
        {
            abrirPuerta.Play();
            accesoPermitido = true;
        }
        else
        {
            accesoPermitido = false;
        }

        if (accesoPermitido == true)
        {
            float tiempoTotal = 1f;
            float tiempoTranscurrido = 0f;
            while (tiempoTranscurrido < tiempoTotal)
            {
                puertaAcceso.transform.rotation = Quaternion.Slerp(puertaAcceso.transform.rotation, Quaternion.Euler(0f, 10f, 0f), tiempoTranscurrido / tiempoTotal);
                tiempoTranscurrido += Time.deltaTime;
            }
        }
        else if (accesoPermitido == false)
        {
            float tiempoTotal = 1f;
            float tiempoTranscurrido = 0f;
            while (tiempoTranscurrido < tiempoTotal)
            {
                puertaAcceso.transform.rotation = Quaternion.Slerp(puertaAcceso.transform.rotation, Quaternion.Euler(0f, -90f, 0f), tiempoTranscurrido / tiempoTotal);
                tiempoTranscurrido += Time.deltaTime;
            }
        }
    }
}
