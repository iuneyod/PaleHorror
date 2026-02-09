using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoMinigame4NoApagarVela : ObjetoInteractivo
{
    public Transform puntoAgarrar;
    public bool agarrandoObjeto = false;
    public Transform puntoSoltar;
    public bool colocarObjeto = false;
    public bool ObjetoColocado = false;
    public AudioSource colocar;

    public override void ActivarObjeto()
    {
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
        if (colocarObjeto == true)
        {
            colocar.Play();
            this.gameObject.transform.SetParent(null);
            this.gameObject.transform.position = new Vector3 (392.38f, 32.422f, 695.251f);
            this.gameObject.transform.rotation = puntoSoltar.rotation;
            agarrandoObjeto = false;
            this.gameObject.tag = "Untagged";
            ObjetoColocado = true;
        }
    }
}
