using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sodared1 : ObjetoInteractivo
{
    public override void ActivarObjeto()
    {
        GameManager.Instance.comidaTotalComida += 1;
        Destroy(gameObject);
    }
}
