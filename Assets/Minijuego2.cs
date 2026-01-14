using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Camera Look")]
    public float sensibilidad = 120f;
    public float limiteArriba = 60f;
    public float limiteAbajo = -60f;

    float rotacionVertical = 0f;




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
        cameraTransform.localRotation = Quaternion.Euler(rotacionVertical, 0F, 0f);

        if (puedeAparecerTornillo)
        {
            StartCoroutine(AparecerTornillo());
            puedeAparecerTornillo = false;
        }

    }


    IEnumerator Monster()
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

            yield return null;
        }

        // Asegurar posición final
        leftHandStart.position = leftHandEnd.position;
        rightHandStart.position = rightHandEnd.position;
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
