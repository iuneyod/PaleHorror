using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DilophosaurioLobby : ObjetoInteractivo
{
    GameManagerMainScene gameManager;
    public AudioSource dino;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManagerMainScene>();
    }
    public override void ActivarObjeto()
    {
        GameManager.Instance.ObtenerObjetoArcade1();

        gameManager.puedeJugarDino = true;
        StartCoroutine(Dino());
    }

    IEnumerator Dino()
    {
        dino.Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(GameManager.Instance.tieneObjetoArcade1 == true)
        {
            Destroy(this.gameObject);
            gameManager.puedeJugarDino = true;
        }
    }


}
