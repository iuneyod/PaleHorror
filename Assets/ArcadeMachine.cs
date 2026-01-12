using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine : ObjetoInteractivo
{
    public override void ActivarObjeto()
    {
        SceneManager.LoadScene("ArcadeMinigame");
    }
}
