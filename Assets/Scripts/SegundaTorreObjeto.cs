using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegundaTorreObjeto : MonoBehaviour
{
    public bool tieneObjetoTorre2 = false;
    public bool enArea = false;



    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            tieneObjetoTorre2 = true;
            Destroy(gameObject);
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
}
