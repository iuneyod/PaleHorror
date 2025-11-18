using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanderaMonumento2 : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool Monumento2_Desbloqueado = false;


    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            Monumento2_Desbloqueado = true;
        }

        if (enArea == true && Monumento2_Desbloqueado == true)
        {
            Particulas.SetActive(true);
        }

        if (!enArea)
        {
            Particulas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }

    public void Monumento()
    {
        if (enArea == true && Monumento2_Desbloqueado)
        {

        }
    }
}
