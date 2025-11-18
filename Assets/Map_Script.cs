using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Script : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool Mapa_Desbloqueado = false;

    private void Start()
    {
        Particulas.SetActive(false);
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

    private void Update()
    {
        Mapa();
    }


    public void Mapa()
    {
        if (enArea == true && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MostrarParticulas());
        }
    }

    public IEnumerator MostrarParticulas()
    {
        Particulas.SetActive(true);
        yield return new WaitForSeconds(2f);
        Particulas.SetActive(false);
        Mapa_Desbloqueado = true;
        Destroy(gameObject);
    }
}
