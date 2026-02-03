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
        GameManager.Instance.ObtenerObjetoArcade2();

        gameManager.puedeJugarBlock = true;
        StartCoroutine(DinoFallBlockCorrutine());
    }

    IEnumerator DinoFallBlockCorrutine()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (GameManager.Instance.tieneObjetoArcade2 == true)
        {
            Destroy(this.gameObject);
            gameManager.puedeJugarBlock = true;
        }
    }
}
