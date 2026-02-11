using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : ObjetoInteractivo
{
    PistolaVentosa pistola;
    Selected selected;
    public bool enMano = false;
    public GameObject basketball;
    public Rigidbody rigidbodyBasketball;
    public Transform manoJugador;
    public float fuerzaLanzamiento = 4f;
    public Camera camaraJugador;

    private void Start()
    {
        pistola = FindAnyObjectByType<PistolaVentosa>();
        selected = FindObjectOfType<Selected>();
    }
    
    public override void ActivarObjeto()
    {
        if (enMano || pistola.pistolaEnMano == true)
        {
            return;
        }

        AgarrarBalon();
    }

    private void Update()
    {
        if (enMano && Input.GetMouseButtonDown(0))
        {
            LanzarBalon();
        }

        if (enMano == true)
        {
            selected.canInteract = false;
        }
    }


    public void AgarrarBalon()
    {
        if (enMano) return;
        enMano = true;
        basketball.transform.SetParent(manoJugador);
        basketball.transform.localPosition = Vector3.zero;
        basketball.transform.localRotation = Quaternion.identity;
        rigidbodyBasketball.isKinematic = true;
        rigidbodyBasketball.useGravity = false;
    }

    public void LanzarBalon()
    {
        if (!enMano) return;

        enMano = false;
        basketball.transform.SetParent(null);

        rigidbodyBasketball.isKinematic = false;
        rigidbodyBasketball.useGravity = true;
        rigidbodyBasketball.WakeUp();

        // Resetear velocidad previa
        rigidbodyBasketball.velocity = Vector3.zero;
        rigidbodyBasketball.angularVelocity = Vector3.zero;

        // Dirección exacta a donde mira el jugador
        Vector3 direccion = camaraJugador.transform.forward;

        rigidbodyBasketball.AddForce(direccion * fuerzaLanzamiento, ForceMode.Impulse);
    }

}
