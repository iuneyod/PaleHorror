using System.Collections;
using UnityEngine;

public class PalancaMinigame4 : ObjetoInteractivo
{
    ObjetoMinigame4NoApagarVela objetoNoApagar;
    VelaMinigame4 velaScript;
    Vela2MInigame4 vela2Script;
    public GameObject palanca;

    bool palancaArriba = true;
    bool palancaAbajo = false;
    bool puedeInteractuar = true;
    bool palancaActivada = false;

    public GameObject cadenaVela;
    public GameObject cadena2;


    Vector3 cadenaVelaArriba;
    Vector3 cadenaVelaAbajo;
    Vector3 cadena2Arriba;
    Vector3 cadena2Abajo;

    Vector3 cadenaVelaAbajoConObjeto;

    public AudioSource cadena;


    void Start()
    {
        velaScript = FindObjectOfType<VelaMinigame4>();
        vela2Script = FindObjectOfType<Vela2MInigame4>();
        objetoNoApagar = FindObjectOfType<ObjetoMinigame4NoApagarVela>();

        cadenaVelaArriba = cadenaVela.transform.localPosition;
        cadenaVelaAbajo = cadenaVelaArriba + new Vector3(0f, -1.55f, 0f);

        cadenaVelaAbajoConObjeto = cadenaVelaArriba + new Vector3(0f, -0.91f, 0f);


        cadena2Arriba = cadena2.transform.localPosition;
        cadena2Abajo = cadena2Arriba + new Vector3(0f, 1f, 0f);
    }

    private void Update()
    {
        
    }


    public override void ActivarObjeto()
    {
        if (!puedeInteractuar) return;

        if (palancaArriba)
            StartCoroutine(BajarPalanca());
        else
            StartCoroutine(SubirPalanca());
    }

    IEnumerator BajarPalanca()
    {
        if (objetoNoApagar.ObjetoColocado == false)
        {
            cadena.Play();
            puedeInteractuar = false;

            Quaternion inicio = palanca.transform.localRotation;
            Quaternion fin = Quaternion.Euler(-78f, -180f, 0f);

            Vector3 cadenaVelaInicio = cadenaVelaArriba;
            Vector3 cadenaVelaFin = cadenaVelaAbajo;

            Vector3 cadena2Inicio = cadena2Arriba;
            Vector3 cadena2Fin = cadena2Abajo;


            float t = 0f;
            float duracion = 2f;

            while (t < duracion)
            {
                float lerp = t / duracion;

                palanca.transform.localRotation =
                    Quaternion.Lerp(inicio, fin, lerp);

                cadenaVela.transform.localPosition =
                    Vector3.Lerp(cadenaVelaInicio, cadenaVelaFin, lerp);

                cadena2.transform.localPosition =
                    Vector3.Lerp(cadena2Inicio, cadena2Fin, lerp);

                t += Time.deltaTime;
                yield return null;
            }

            velaScript.velaEncendida = false;
            velaScript.puedeEncenderla = false;
            velaScript.fire.SetActive(false);

            vela2Script.vela2Encendida = true;
            vela2Script.fire2.SetActive(true);

            palanca.transform.localRotation = fin;

            palancaArriba = false;
            palancaAbajo = true;
            puedeInteractuar = true;
            palancaActivada = false;
        }
        else if (objetoNoApagar.ObjetoColocado == true)
        {
            cadena.Play();
            puedeInteractuar = false;

            Quaternion inicio = palanca.transform.localRotation;
            Quaternion fin = Quaternion.Euler(-78f, -180f, 0f);

            Vector3 cadenaVelaInicio = cadenaVelaArriba;
            Vector3 cadenaVelaFin = cadenaVelaAbajoConObjeto;

            Vector3 cadena2Inicio = cadena2Arriba;
            Vector3 cadena2Fin = cadena2Abajo;


            float t = 0f;
            float duracion = 2f;

            while (t < duracion)
            {
                float lerp = t / duracion;

                palanca.transform.localRotation =
                    Quaternion.Lerp(inicio, fin, lerp);

                cadenaVela.transform.localPosition =
                    Vector3.Lerp(cadenaVelaInicio, cadenaVelaFin, lerp);

                cadena2.transform.localPosition =
                    Vector3.Lerp(cadena2Inicio, cadena2Fin, lerp);

                t += Time.deltaTime;
                yield return null;
            }

            vela2Script.vela2Encendida = true;
            vela2Script.fire2.SetActive(true);

            palanca.transform.localRotation = fin;

            palancaArriba = false;
            palancaAbajo = true;
            puedeInteractuar = true;
            palancaActivada = false;
        }

        
    }

    IEnumerator SubirPalanca()
    {
        if (objetoNoApagar.ObjetoColocado == false)
        {
            cadena.Play();
            puedeInteractuar = false;

            vela2Script.vela2Encendida = false;
            vela2Script.fire2.SetActive(false);

            Quaternion inicio = palanca.transform.localRotation;
            Quaternion fin = Quaternion.Euler(60f, -180f, 0f);

            Vector3 cadenaVelaInicio = cadenaVelaAbajo;
            Vector3 cadenaVelaFin = cadenaVelaArriba;

            Vector3 cadena2Inicio = cadena2Abajo;
            Vector3 cadena2Fin = cadena2Arriba;


            float t = 0f;
            float duracion = 2f;

            while (t < duracion)
            {
                float lerp = t / duracion;

                palanca.transform.localRotation =
                    Quaternion.Lerp(inicio, fin, lerp);

                cadenaVela.transform.localPosition =
                    Vector3.Lerp(cadenaVelaInicio, cadenaVelaFin, lerp);

                cadena2.transform.localPosition =
                    Vector3.Lerp(cadena2Inicio, cadena2Fin, lerp);

                t += Time.deltaTime;
                yield return null;
            }

            velaScript.puedeEncenderla = true;


            palanca.transform.localRotation = fin;

            palancaArriba = true;
            palancaAbajo = false;
            puedeInteractuar = true;
            palancaActivada = false;
        }
        else if(objetoNoApagar.ObjetoColocado == true)
        {
            cadena.Play();
            puedeInteractuar = false;

            vela2Script.vela2Encendida = false;
            vela2Script.fire2.SetActive(false);

            Quaternion inicio = palanca.transform.localRotation;
            Quaternion fin = Quaternion.Euler(60f, -180f, 0f);

            Vector3 cadenaVelaInicio = cadenaVelaAbajoConObjeto;
            Vector3 cadenaVelaFin = cadenaVelaArriba;

            Vector3 cadena2Inicio = cadena2Abajo;
            Vector3 cadena2Fin = cadena2Arriba;


            float t = 0f;
            float duracion = 2f;

            while (t < duracion)
            {
                float lerp = t / duracion;

                palanca.transform.localRotation =
                    Quaternion.Lerp(inicio, fin, lerp);

                cadenaVela.transform.localPosition =
                    Vector3.Lerp(cadenaVelaInicio, cadenaVelaFin, lerp);

                cadena2.transform.localPosition =
                    Vector3.Lerp(cadena2Inicio, cadena2Fin, lerp);

                t += Time.deltaTime;
                yield return null;
            }

            velaScript.puedeEncenderla = true;


            palanca.transform.localRotation = fin;

            palancaArriba = true;
            palancaAbajo = false;
            puedeInteractuar = true;
            palancaActivada = false;
        }

        
    }
}
