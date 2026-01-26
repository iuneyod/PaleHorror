using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelaMinigame4 : ObjetoInteractivo
{
    public GameObject fire;
    public bool velaEncendida = false;
    public bool puedeEncenderla = false;

    private void Start()
    {
        fire.SetActive(false);
    }

    public override void ActivarObjeto()
    {
        if (puedeEncenderla && !velaEncendida)
        {
            fire.SetActive(true);
            velaEncendida = true;
        }
    }

    void Update()
    {
        
    }
}
