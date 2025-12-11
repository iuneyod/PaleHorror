using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class RadioTower1 : MonoBehaviour
{
    public bool enArea = false;
    public bool Radio1Active = false;

    public GameObject areaEffect;
    public GameObject ElectricEffect;

    public BoxCollider areaCollider;

    PalancaCasaSolitaria PalancaCasaSolitaria;

    void Start()
    {
        PalancaCasaSolitaria palancaScript = FindObjectOfType<PalancaCasaSolitaria>();
    }


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && PalancaCasaSolitaria.palancaCasaSolitariaActive == true)
        {
            Radio1Active = true;
            areaCollider.enabled = false;
            StartCoroutine(StartEffect());
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

    IEnumerator StartEffect()
    {
        areaEffect.SetActive(true);
        ElectricEffect.SetActive(true);
        yield return new WaitForSeconds(15f);
        areaEffect.SetActive(false);
        ElectricEffect.SetActive(false);
    }
}
