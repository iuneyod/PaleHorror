using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaDerechaMansion : ObjetoInteractivo
{
    private bool abierta = false;
    public int anguloApertura;
    public Quaternion rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.rotation;
    }


    public override void ActivarObjeto()
    {
        if (!abierta)
        {
            AbrirPuerta();
            abierta = true;
            this.gameObject.isStatic = true;
        }
    }

    private void AbrirPuerta()
    {
       this.gameObject.transform.rotation = Quaternion.Euler(0, anguloApertura, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, anguloApertura, 0);
            this.gameObject.isStatic = true;
            abierta = true;
        }
    }

}
