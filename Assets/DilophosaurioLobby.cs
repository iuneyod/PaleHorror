using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DilophosaurioLobby : ObjetoInteractivo
{
    GameManagerMainScene gameManager;
    public Transform puntoSpawnDino;
    public BoxCollider colliderDino;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManagerMainScene>();
        colliderDino = GetComponent<BoxCollider>();
    }
    public override void ActivarObjeto()
    {
        gameManager.puedeJugarDino = true;
        StartCoroutine(Dino());
    }

    IEnumerator Dino()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
