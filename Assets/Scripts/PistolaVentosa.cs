using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaVentosa : ObjetoInteractivo
{

    public GameObject carryPoint;
    public GameObject ventosa;
    public Transform shootPoint;
    private bool pistolaEnMano = false;
    Selected selected;
    public Rigidbody rb;
    public Camera camaraJugador;

    private void Start()
    {
        selected = FindObjectOfType<Selected>();
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
            selected.canInteract = false;
        }

        if(pistolaEnMano == true && Input.GetKeyDown(KeyCode.Q))
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
