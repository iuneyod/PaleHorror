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

    PrimeraTorreObjeto torre1Objeto;

    void Start()
    {
        torre1Objeto = FindObjectOfType<PrimeraTorreObjeto>();
    }


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && torre1Objeto.tieneObjetoTorre1 == true)
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
