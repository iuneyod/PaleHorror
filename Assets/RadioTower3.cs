using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower3 : MonoBehaviour
{
    public bool enArea = false;
    public bool Radio3Active = false;

    public GameObject areaEffect;
    public GameObject ElectricEffect;

    public BoxCollider areaCollider;

    PalancaInvernadero palancaInvernadero;

    void Start()
    {
        palancaInvernadero = FindObjectOfType<PalancaInvernadero>();
    }


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && palancaInvernadero.palancaInvernaderoActive == true)
        {
            Radio3Active = true;
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
