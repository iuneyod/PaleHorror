using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : ObjetoInteractivo
{
    public GameObject ArcadeUI;
    public bool pantallaAbierta = false;
    public override void ActivarObjeto()
    {
        ArcadeUI.SetActive(true);
        pantallaAbierta = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ArcadeUI.SetActive(false);
            pantallaAbierta = false;
        }
    }
}
