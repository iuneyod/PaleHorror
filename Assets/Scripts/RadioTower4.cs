using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower4 : MonoBehaviour
{
    public bool enArea = false;
    public bool Radio4Active = false;

    public GameObject areaEffect;
    public GameObject ElectricEffect;

    public BoxCollider areaCollider;

    CuartaTorreObjeto torre4Objeto;

    void Start()
    {
        torre4Objeto = FindObjectOfType<CuartaTorreObjeto>();
    }


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && torre4Objeto.tieneObjetoTorre4 == true)
        {
            Radio4Active = true;
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
