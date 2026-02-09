using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : ObjetoInteractivo
{
    Selected selected;
    public GameObject ArcadeUI;
    public bool pantallaAbierta = false;
    public bool acabaDeCerrarArcade = false;

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
        if (Input.GetKeyDown(KeyCode.Escape) && pantallaAbierta)
        {
            ArcadeUI.SetActive(false);
            pantallaAbierta = false;
            acabaDeCerrarArcade = true;
            StartCoroutine(ResetCerrarFlag());
        }

        selected.sistemaActivo = !ArcadeUI.activeSelf;
    }

    IEnumerator ResetCerrarFlag()
    {
        yield return null; // espera 1 frame
        acabaDeCerrarArcade = false;
    }
}
