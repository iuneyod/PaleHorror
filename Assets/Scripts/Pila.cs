using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila : ObjetoInteractivo
{
    MINIGAME3 minigame3;

    private void Start()
    {
        minigame3 = FindObjectOfType<MINIGAME3>();
    }

    public override void ActivarObjeto()
    {
        minigame3.duracionLinterna += 60f;
        Destroy(gameObject);
    }
}
