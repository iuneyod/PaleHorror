using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower2 : MonoBehaviour
{
    public bool enArea = false;
    public bool Radio2Active = false;

    public GameObject areaEffect;
    public GameObject ElectricEffect;

    public BoxCollider areaCollider;

    PalancaCasaVillage palancaVillage;



    void Start()
    {
        palancaVillage = FindObjectOfType<PalancaCasaVillage>();
    }


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && palancaVillage.palancaCasaVillageActive == true)
        {
            Radio2Active = true;
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
