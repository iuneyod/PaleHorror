using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : ObjetoInteractivo
{
    Selected selected;
    public GameObject ArcadeUI;
    public bool pantallaAbierta = false;

    private void Start()
    {
        selected = FindObjectOfType<Selected>();
        ArcadeUI.SetActive(false);
    }

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

        if(ArcadeUI.activeSelf == true)
        {
            selected.sistemaActivo = false;
        }
        else
        {
            selected.sistemaActivo = true;
        }
    }
}
