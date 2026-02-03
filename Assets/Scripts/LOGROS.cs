using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOGROS : MonoBehaviour
{
    FnafHouseManager fnafHouseManager;
    CollisionSalida collisionSalida;
    CollisionSalida4 collisionSalida4;
    Sodared1 sodared1;
    GameManagerMainScene gameManagerMainScene;
    VentMinigame2 ventMinigame2;
    Basket basket;
    public bool CompletaElPrimerMinijuego = false;
    public bool EncuentraLosDosJuegosOcultosDeLaArcade = false;
    public bool Comete10Latas = false;
    public bool CompletaTodosLosMinijuegos = false;
    public bool CompletaTodosLosMinijuegosEnDificil = false;
    public bool Encesta5VecesEnLaCanasta = false;

    //public GameObject TickCompletado;

    private void Start()
    {
        fnafHouseManager = FindObjectOfType<FnafHouseManager>();
        sodared1 = FindObjectOfType<Sodared1>();
        gameManagerMainScene = FindObjectOfType<GameManagerMainScene>();
        ventMinigame2 = FindObjectOfType<VentMinigame2>();
        collisionSalida = FindObjectOfType<CollisionSalida>();
        collisionSalida4 = FindObjectOfType<CollisionSalida4>();
        basket = FindObjectOfType<Basket>();
    }



    private void Update()
    {
        if(fnafHouseManager.minigame1Completado == true)
        {
            CompletaElPrimerMinijuego = true;
        }
        if (gameManagerMainScene.puedeJugarDino == true && gameManagerMainScene.puedeJugarBlock == true)
        {
            EncuentraLosDosJuegosOcultosDeLaArcade = true;
        }
        if (fnafHouseManager.minigame1Completado == true && ventMinigame2.minijuego2Completado == true && collisionSalida.minijuego3Completado == true && collisionSalida4.minijuego4Completado == true)
        {
            CompletaTodosLosMinijuegos = true;
        }
        if(basket.puntos >= 5)
        {
            Encesta5VecesEnLaCanasta = true;
        }
    }
}
