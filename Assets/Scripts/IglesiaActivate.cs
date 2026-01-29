using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglesiaActivate : MonoBehaviour
{
    public bool enIglesia = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enIglesia = true;
        }
    }
}
