using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerCoche : ObjetoInteractivo
{
    public Transform puntoAgarrar;
    public bool agarrandoObjeto = false;
    public Transform puntoSoltar;
    public bool colocarObjeto = false;
    public bool ObjetoColocado = false;


    public override void ActivarObjeto()
    {
        if (ObjetoColocado == true)
            return;

        AgarrarObjeto();
    }

    void AgarrarObjeto()
    {
        this.gameObject.transform.position = puntoAgarrar.position;
        this.gameObject.transform.rotation = puntoAgarrar.rotation;
        this.gameObject.transform.SetParent(puntoAgarrar);
        agarrandoObjeto = true;
    }

    private void Update()
    {
        if (!colocarObjeto) return;

        transform.SetParent(null);
        transform.position = puntoSoltar.position;
        transform.rotation = puntoSoltar.rotation;

        agarrandoObjeto = false;
        ObjetoColocado = true;
        colocarObjeto = false;
        gameObject.tag = "Untagged";
    }
}
