using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanderaMonumentoCuracion : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool Monumento4_Desbloqueado = false;


    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            Monumento4_Desbloqueado = true;
        }

        if (enArea == true && Monumento4_Desbloqueado == true)
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
        if (enArea == true && Monumento4_Desbloqueado)
        {

        }
    }
}
