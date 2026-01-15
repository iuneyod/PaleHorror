using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minijuego2 : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraPlayer;
    public GameObject Light;
    public Transform cameraTransform;

    //Tornillo
    public GameObject Tornillo;
    public bool haTocadoTornillo = false;
    public Transform puntoTornillo;
    public bool hayTornillo = false;
    public bool puedeAparecerTornillo = true;

    public Transform leftHandStart;
    public Transform rightHandStart;
    public Transform leftHandEnd;
    public Transform rightHandEnd;
    public GameObject monster;
    private bool isAttacking = false;
    private bool puedeAtacar = true;

    [Header("Camera Look")]
    public float sensibilidad = 120f;
    public float limiteArriba = 60f;
    public float limiteAbajo = -60f;

    float rotacionVertical = 0f;

    public GameObject TeclaW;
    public GameObject TeclaS;
    public GameObject TeclaF;
    public GameObject TeclaA;
    public GameObject TeclaD;

    [Header("QTE")]
    public float tiempoPorTecla = 3f;
    public int teclasNecesarias = 4;

    private bool QTEActivo = false;


    KeyCode GetRandomKey()
    {
        KeyCode[] teclas = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.R };
        return teclas[Random.Range(0, teclas.Length)];
    }
    void MostrarTecla(KeyCode key)
    {
        TeclaW.SetActive(key == KeyCode.W);
        TeclaA.SetActive(key == KeyCode.A);
        TeclaS.SetActive(key == KeyCode.S);
        TeclaD.SetActive(key == KeyCode.D);
        TeclaF.SetActive(key == KeyCode.F);
    }

    void OcultarTeclas()
    {
        TeclaW.SetActive(false);
        TeclaA.SetActive(false);
        TeclaS.SetActive(false);
        TeclaD.SetActive(false);
        TeclaF.SetActive(false);
    }





    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Monster());
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && isAttacking == false)
        {
            Vector3 direccion = player.transform.forward;
            direccion.y = 0f;
            direccion.Normalize();

            player.transform.position += direccion * 1.2f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) && isAttacking == false)
        {
            Vector3 direccion = player.transform.forward;
            direccion.y = 0f;
            direccion.Normalize();
            player.transform.position -= direccion * 1f * Time.deltaTime;
        }


        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        // Invertimos porque Unity invierte el eje
        rotacionVertical -= mouseY;

        // Clamp (límite de giro)
        rotacionVertical = Mathf.Clamp(rotacionVertical, limiteAbajo, limiteArriba);

        // Aplicamos SOLO al eje X (mirar arriba/abajo)
        cameraTransform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        if (puedeAparecerTornillo)
        {
            StartCoroutine(AparecerTornillo());
            puedeAparecerTornillo = false;
        }

    }


    IEnumerator Monster()
    {
        while(puedeAtacar == true)
        {
            if (Random.Range(0, 4) == 0)
                yield break;

            yield return new WaitForSeconds(2f); // Delay antes de atacar
            isAttacking = true;

            float tiempo = 0f;
            float duracion = 0.4f;

            Vector3 leftStart = leftHandStart.position;
            Vector3 rightStart = rightHandStart.position;

            while (tiempo < duracion)
            {
                tiempo += Time.deltaTime;
                float t = tiempo / duracion;

                leftHandStart.position = Vector3.Lerp(leftStart, leftHandEnd.position, t);
                rightHandStart.position = Vector3.Lerp(rightStart, rightHandEnd.position, t);
                yield return StartCoroutine(QTESystem());

                yield return null;
            }

            // Asegurar posición final
            leftHandStart.position = leftHandEnd.position;
            rightHandStart.position = rightHandEnd.position;
        }
    }
    IEnumerator RetirarManos()
    {
        float tiempo = 0f;
        float duracion = 0.4f;

        Vector3 leftStart = leftHandStart.position;
        Vector3 rightStart = rightHandStart.position;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            leftHandStart.position = Vector3.Lerp(leftStart, leftHandStart.position, t);
            rightHandStart.position = Vector3.Lerp(rightStart, rightHandStart.position, t);
            yield return null;
        }

        isAttacking = false;
    }

    IEnumerator QTESystem()
    {
        QTEActivo = true;
        int aciertos = 0;

        while (aciertos < teclasNecesarias)
        {
            KeyCode teclaActual = GetRandomKey();
            MostrarTecla(teclaActual);

            float tiempo = 0f;
            bool pulsada = false;

            while (tiempo < tiempoPorTecla)
            {
                if (Input.GetKeyDown(teclaActual))
                {
                    pulsada = true;
                    break;
                }

                tiempo += Time.deltaTime;
                yield return null;
            }

            if (!pulsada)
            {
                QTEActivo = false;
                OcultarTeclas();
                yield break;
            }

            aciertos++;
            OcultarTeclas();
            yield return new WaitForSeconds(0.15f); // pequeño respiro visual
        }

        QTEActivo = false;
        StartCoroutine(RetirarManos());
    }


    IEnumerator AparecerTornillo()
    {
        while (hayTornillo == false)
        {

            yield return new WaitForSeconds(2f);
            float probabilidad = Random.Range(0f, 1f);
            if (probabilidad < 0.5f && !haTocadoTornillo)
            {
                Instantiate(Tornillo, puntoTornillo.position, puntoTornillo.rotation);
                hayTornillo = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tornillo"))
        {
            haTocadoTornillo = true;
        }
    }
}
