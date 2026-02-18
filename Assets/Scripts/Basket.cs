using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Basket : MonoBehaviour
{
    public int puntos = 0;

    public AudioSource canasta;
    BasketBall basketBall;

    private void Start()
    {
        basketBall = FindObjectOfType<BasketBall>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoInteractivo") && basketBall.enMano == false)
        {
            puntos ++;
            canasta.Play();
        }
    }

    void Update()
    {
        if (puntos >= 5)
        {
            GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.Canasta_5);
            GameManager.Instance.MostrarAvisoLogro(LogrosID.Canasta_5, "¿BASKETBOLISTA?");
        }
    }
}