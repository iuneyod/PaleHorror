using System.Collections;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.UI;

public class GeneradorMansion : ObjetoInteractivo
{
    ThirdPersonController controller;
    MINIGAME3 minigame3;

    public float progresoReparacion = 0f;
    public float progresoMaximo = 100f;
    public Transform puntoReparacion;

    public bool reparando = false;

    public GameObject mainCamera;
    public GameObject controlCamara;

    Vector3 camLocalPosInicial;
    Quaternion camLocalRotInicial;

    Coroutine repararRoutine;

    public TMP_Text porcentajeReparado;
    public Image barraProgreso;

    public AudioSource generator;
    public Image clickIzquierdo;

    void Start()
    {
        controller = FindObjectOfType<ThirdPersonController>();
        minigame3 = FindObjectOfType<MINIGAME3>();
        porcentajeReparado.gameObject.SetActive(false);
        barraProgreso.gameObject.SetActive(false);

        HacerTodoStatic(transform);
        clickIzquierdo.gameObject.SetActive(false);
    }

    void HacerTodoStatic(Transform root)
    {
        root.gameObject.isStatic = true;

        foreach (Transform hijo in root)
        {
            HacerTodoStatic(hijo);
        }
    }

    public override void ActivarObjeto()
    {
        if (progresoReparacion >= progresoMaximo)
            return;
        if (!reparando)
            repararRoutine = StartCoroutine(RepararGenerador());
    }

    void Update()
    {
        if (reparando && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelarReparacion();
            generator.Stop();
        }

        if (porcentajeReparado != null && reparando)
        {
            porcentajeReparado.gameObject.SetActive(true);
            float porcentaje = (progresoReparacion / progresoMaximo) * 100f;
            porcentaje = Mathf.Clamp(porcentaje, 0f, 100f);
            porcentajeReparado.text = porcentaje.ToString("F2") + "%";
            barraProgreso.gameObject.SetActive(true);
            barraProgreso.fillAmount = progresoReparacion / progresoMaximo;
        }


        if (progresoReparacion >= progresoMaximo)
        {
            progresoReparacion = progresoMaximo;
        }
        
    }

    IEnumerator RepararGenerador()
    {
        reparando = true;
        clickIzquierdo.gameObject.SetActive(true);
        generator.Play();
        controller.MoveSpeed = 0f;
        controller.SprintSpeed = 0f;
        controller.LockCameraPosition = true;
        minigame3.puedeUsarLinterna = false;

        camLocalPosInicial = mainCamera.transform.localPosition;
        camLocalRotInicial = mainCamera.transform.localRotation;

        mainCamera.transform.parent = null;

        // Mover cámara
        float t = 0f;
        float duracion = 0.5f;
        Vector3 posIni = mainCamera.transform.position;
        Quaternion rotIni = mainCamera.transform.rotation;

        while (t < duracion)
        {
            mainCamera.transform.position = Vector3.Lerp(posIni, puntoReparacion.position, t / duracion);
            mainCamera.transform.rotation = Quaternion.Lerp(rotIni, puntoReparacion.rotation, t / duracion);
            t += Time.deltaTime;
            yield return null;
        }

        // Reparación real
        while (progresoReparacion < progresoMaximo && reparando)
        {
            if (Input.GetMouseButton(0))
            {
                progresoReparacion += 8f * Time.deltaTime;
                minigame3.ruido += 30f * Time.deltaTime;
            }

            yield return null;
        }

        if (reparando)
            FinalizarReparacion();
    }


    void CancelarReparacion()
    {
        reparando = false;
        clickIzquierdo.gameObject.SetActive(false);
        porcentajeReparado.gameObject.SetActive(false);
        barraProgreso.gameObject.SetActive(false);
        generator.Stop();

        if (repararRoutine != null)
            StopCoroutine(repararRoutine);

        ResetJugador();
    }


    void FinalizarReparacion()
    {
        generator.Stop();
        clickIzquierdo.gameObject.SetActive(false);
        minigame3.ruido += 100f;
        minigame3.generadoresReparados++;
        reparando = false;
        porcentajeReparado.gameObject.SetActive(false);
        barraProgreso.gameObject.SetActive(false);
        ResetJugador();
        Debug.Log("Generador reparado");
    }

    void ResetJugador()
    {
        mainCamera.transform.parent = controlCamara.transform;

        mainCamera.transform.localPosition = camLocalPosInicial;
        mainCamera.transform.localRotation = camLocalRotInicial;

        controller.MoveSpeed = 4f;
        controller.SprintSpeed = 6f;
        controller.LockCameraPosition = false;
        minigame3.puedeUsarLinterna = true;
    }

}
