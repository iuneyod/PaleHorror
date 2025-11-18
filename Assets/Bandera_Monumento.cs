using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandera_Monumento : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool Monumento1_Desbloqueado = false;

    [SerializeField]
    Bonfire bonfire;

    void Start()
    {
        //bonfire = FindObjectOfType<Bonfire>();
    }


    void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E) && bonfire.BonfireFinished1 == true)
        {
            Monumento1_Desbloqueado = true;
        }

        if (enArea == true && Monumento1_Desbloqueado == true)
        {
            Particulas.SetActive(true);
        }

        if (!enArea)
        {
            Particulas.SetActive(false);
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

    public void Monumento()
    {
        if (enArea == true && Monumento1_Desbloqueado)
        {
            
        }
    }
}
