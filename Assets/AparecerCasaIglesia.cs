using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerCasaIglesia : MonoBehaviour
{
    public GameObject CasaIglesia;
    public GameObject Efectos;
    public BoxCollider TriggerCollider;


    private void Start()
    {
        CasaIglesia.SetActive(false);
        Efectos.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(AparecerEdificio());
            TriggerCollider.enabled = false; // Desactiva el trigger para que no se repita
        }
    }

    private IEnumerator AparecerEdificio()
    {
        CasaIglesia.SetActive(true);
        Efectos.SetActive(true);
        yield return new WaitForSeconds(15f);
        Efectos.SetActive(false);
        CasaIglesia.SetActive(false);
    }
}

