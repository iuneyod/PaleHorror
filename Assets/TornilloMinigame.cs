using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornilloMinigame : ObjetoInteractivo
{
    Rigidbody rb;
    public int fuerzaLanzamiento;
    Minijuego2 minijuego2;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        minijuego2 = FindObjectOfType<Minijuego2>();
    }
    public override void ActivarObjeto()
    {
        minijuego2.hayTornillo = false;
        minijuego2.puedeAparecerTornillo = true;

        int randomY = Random.Range(0, 2);

        if (randomY == 1)
        {
            rb.AddForce(Vector3.left * fuerzaLanzamiento, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * fuerzaLanzamiento, ForceMode.Impulse);
        }

        StartCoroutine(DestruirTornillo());
    }


    IEnumerator DestruirTornillo()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
