using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teddy : MonoBehaviour
{


    public bool enArea = false;
    //public GameObject throwEffect;
    public TMP_Text interactText;
    public BoxCollider trigger;

    public bool activatedTeddyTrap = false;

    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            interactText.gameObject.SetActive(false);
            //StartCoroutine(StartEffect());
            activatedTeddyTrap = true;
        }
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

    //IEnumerator StartEffect()
    //{
    //    throwEffect.SetActive(true);
    //    yield return new WaitForSeconds(1.5f);
    //    throwEffect.SetActive(false);
    //}
}
