using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageActivate : MonoBehaviour
{
    public bool enVillage = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enVillage = true;
        }
    }
}
