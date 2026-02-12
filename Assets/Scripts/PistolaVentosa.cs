using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaVentosa : ObjetoInteractivo
{
    BasketBall basket;
    public GameObject carryPoint;
    public GameObject ventosa;
    public Transform shootPoint;
    public bool pistolaEnMano = false;
    Selected selected;
    public Rigidbody rb;
    public Camera camaraJugador;
    public AudioSource shoot;
    public GameObject TeclaQ;

    private void Start()
    {
        basket = GetComponent<BasketBall>();
        selected = FindObjectOfType<Selected>();
        TeclaQ.SetActive(false);
    }

    public override void ActivarObjeto()
    {
        if(pistolaEnMano)
        {
            return;
        }

        CogerPistola();

    }

    public void Update()
    {
        if (pistolaEnMano == true && Input.GetMouseButtonDown(0))
        {
            Disparar();
        }

        if (pistolaEnMano == true)
        {
            TeclaQ.SetActive(true);
            selected.canInteract = false;
        }
        else
        {
            TeclaQ.SetActive(false);
        }

        if (pistolaEnMano == true && Input.GetKeyDown(KeyCode.Q))
        {
            pistolaEnMano = false;
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    void CogerPistola()
    {
        pistolaEnMano = true;

        transform.SetParent(carryPoint.transform);
        transform.localPosition = Vector3.zero;

        // Rotación correcta (ver punto 2)
        transform.localRotation = Quaternion.Euler(0, -90f, 0);

        rb.isKinematic = true;
        rb.useGravity = false;
    }


    void Disparar()
    {
        shoot.Play();
        GameObject ventosaInstance = Instantiate(ventosa, shootPoint.position, Quaternion.identity);
        Rigidbody rb = ventosaInstance.GetComponent<Rigidbody>();

        Vector3 direccion = camaraJugador.transform.forward;

        // Rotación base mirando al frente
        Quaternion rotacion = Quaternion.LookRotation(direccion);

        rotacion *= Quaternion.Euler(90, 0, 90f);

        ventosaInstance.transform.rotation = rotacion;

        rb.velocity = direccion * 10f;
    }


}
