using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PalancaInvernadero : MonoBehaviour
{
    public bool enArea = false;
    public bool palancaInvernaderoActive = false;
    public GameObject palanca;
    public GameObject areaEffect;
    public bool activated = false;
    public BoxCollider trigger;


    public TMP_Text interactText;



    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && activated == false)
        {
            palancaInvernaderoActive = true;
            palanca.transform.Rotate(+50f, 0f, 0f);
            StartCoroutine(StartEffect());
            interactText.gameObject.SetActive(false);
            activated = true;
            trigger.enabled = false;
        }
    }

    IEnumerator StartEffect()
    {
        areaEffect.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        areaEffect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
