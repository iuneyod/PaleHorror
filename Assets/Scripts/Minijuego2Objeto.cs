using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minijuego2Objeto : MonoBehaviour
{
    public bool enArea = false;
    //public GameObject throwEffect;
    public TMP_Text interactText;
    public BoxCollider trigger;

    public bool activatedMinigame2 = false;

    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            //cambiamos a otra escena
            SceneManager.LoadScene("Minigame2");
            interactText.gameObject.SetActive(false);
            //StartCoroutine(StartEffect());
            activatedMinigame2 = true;

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
