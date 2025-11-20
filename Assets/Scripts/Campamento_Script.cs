using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campamento_Script : MonoBehaviour
{
    public bool enArea = false;
    public bool Campamento_Desbloqueado = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
            Campamento_Desbloqueado = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }
}

