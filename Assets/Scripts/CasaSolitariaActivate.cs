using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaSolitariaActivate : MonoBehaviour
{
    public bool enCasaSolitaria = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enCasaSolitaria = true;
        }
    }
}
