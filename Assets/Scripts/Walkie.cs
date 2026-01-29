using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkie : MonoBehaviour
{
    public bool enArea = false;
    public bool tieneWalkie = false;
    public bool walkieEnMano = false;
    public Transform carryPoint;
    public GameObject WalkiePrefab;

    void Update()
    {
        // RECOGER WALKIE
        if (enArea && Input.GetKeyDown(KeyCode.E) && !tieneWalkie)
        {
            tieneWalkie = true;
            Destroy(gameObject); // Elimina el walkie del suelo
        }
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }
}
