using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys_Spawner : MonoBehaviour
{
    public GameObject Efectos;
    public GameObject Enemy;
    public Transform[] SpawnPoints;
    public bool enArea = false;
    public bool puedenSpawnear = false;
    public bool Victoria = false;


    private void Start()
    {
        Efectos.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    private void Update()
    {
        if (enArea)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        StartCoroutine(EfectosDeEntrada());
        if(puedenSpawnear)
        {
            int spawnIndex = Random.Range(0, SpawnPoints.Length);
            Instantiate(Enemy, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation);
            puedenSpawnear = false;
        }

        //si no quedan enemigos vivos en el area, desactivar efectos
        //if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        //{
        //    Efectos.SetActive(false);
        //    enArea = false;
        //    Victoria = true;
        //}
    }

    public IEnumerator EfectosDeEntrada()
    {
        Efectos.SetActive(true);
        yield return new WaitForSeconds(2f);
        puedenSpawnear = true;
    }
}
