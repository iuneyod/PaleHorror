using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public int puntos = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoInteractivo"))
        {
            puntos ++;
        }
    }

    void Update()
    {
        if (puntos >= 5)
        {
            GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.Canasta_5);
        }
    }
}