using System.Collections;
using UnityEngine;

public class PalancaMinigame4 : ObjetoInteractivo
{
    VelaMinigame4 velaScript;
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


    void Start()
    {
        velaScript = FindObjectOfType<VelaMinigame4>();

        cadenaVelaArriba = cadenaVela.transform.localPosition;
        cadenaVelaAbajo = cadenaVelaArriba + new Vector3(0f, -1.55f, 0f);

        cadena2Arriba = cadena2.transform.localPosition;
        cadena2Abajo = cadena2Arriba + new Vector3(0f, 1.55f, 0f);
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
        puedeInteractuar = false;

        Quaternion inicio = palanca.transform.localRotation;
        Quaternion fin = Quaternion.Euler(-78f, -180f, 0f);

        Vector3 cadenaVelaInicio = cadenaVelaArriba;
        Vector3 cadenaVelaFin = cadenaVelaAbajo;

        Vector3 cadena2Inicio = cadena2Arriba;
        Vector3 cadena2Fin = cadena2Abajo;


        float t = 0f;
        float duracion = 1f;

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

        palanca.transform.localRotation = fin;

        palancaArriba = false;
        palancaAbajo = true;
        puedeInteractuar = true;
        palancaActivada = false;
    }

    IEnumerator SubirPalanca()
    {
        puedeInteractuar = false;

        Quaternion inicio = palanca.transform.localRotation;
        Quaternion fin = Quaternion.Euler(60f, -180f, 0f);

        Vector3 cadenaVelaInicio = cadenaVelaAbajo;
        Vector3 cadenaVelaFin = cadenaVelaArriba;

        Vector3 cadena2Inicio = cadena2Abajo;
        Vector3 cadena2Fin = cadena2Arriba;


        float t = 0f;
        float duracion = 1f;

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
