using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuartaTorreObjeto : MonoBehaviour
{
    public bool tieneObjetoTorre4 = false;
    public bool enArea = false;



    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            tieneObjetoTorre4 = true;
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
