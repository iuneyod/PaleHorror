using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JEFE1_Effects : MonoBehaviour
{
    public GameObject Primero_Efecto;
    public GameObject Segundo_Efecto;
    public GameObject Tercero_Efecto;
    public GameObject Cuarto_Efecto;
    public GameObject Ultimo_Efecto;
    public GameObject PortalVerde;
    public GameObject BombaHumo;
    public GameObject JefePrefab;
    public Transform JefeSpawn;

    public bool yaSpawned = false;

    Player_Stats playerStats;

    private void Start()
    {
        Primero_Efecto.SetActive(false);
        Segundo_Efecto.SetActive(false);
        Tercero_Efecto.SetActive(false);
        Cuarto_Efecto.SetActive(false);
        //PortalVerde.SetActive(false);
        BombaHumo.SetActive(false);

        playerStats = FindObjectOfType<Player_Stats>();
    }

    private void Update()
    {
        if (playerStats.isDead == true)
        {
            yaSpawned = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(yaSpawned== false)
            {
                StartCoroutine(EmpezarEfectos());
            }
        }

    }

    public IEnumerator EmpezarEfectos()
    {
        BombaHumo.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PortalVerde.SetActive(false);
        yield return new WaitForSeconds(1f);
        BombaHumo.SetActive(false);
        yield return new WaitForSeconds(2f);
        Primero_Efecto.SetActive(true);
        yield return new WaitForSeconds(2f);
        Segundo_Efecto.SetActive(true);
        yield return new WaitForSeconds(2f);
        Tercero_Efecto.SetActive(true);
        yield return new WaitForSeconds(2f);
        Cuarto_Efecto.SetActive(true);
        JefePrefab.transform.position = JefeSpawn.position;
        yaSpawned = true;
        yield return new WaitForSeconds(0.5f);
        Cuarto_Efecto.SetActive(false);
        Primero_Efecto.SetActive(false);
        Segundo_Efecto.SetActive(false);
        Tercero_Efecto.SetActive(false);
        Ultimo_Efecto.SetActive(true);
        yield return new WaitForSeconds(1f);
        Ultimo_Efecto.SetActive(false);
    }
}
