using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeMinigameArcadeBlock : ObjetoInteractivo
{
    GameManagerMainScene gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerMainScene>();
    }

    public override void ActivarObjeto()
    {
        gameManager.puedeJugarBlock = true;
        StartCoroutine(DinoFallBlockCorrutine());
    }

    IEnumerator DinoFallBlockCorrutine()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
