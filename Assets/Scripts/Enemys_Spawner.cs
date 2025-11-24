using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys_Spawner : MonoBehaviour
{
    public GameObject Efectos;
    public GameObject Enemy;
    public Transform[] SpawnPoints;

    public bool enArea = false;
    public bool yaSpawnearon = false;  // <--- NUEVO
    public int enemigosMaximos = 6;    // <--- NUEVO
    private int enemigosActuales = 0;  // <--- NUEVO

    private void Start()
    {
        Efectos.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;

            if (!yaSpawnearon)   // evita que respawnee infinitamente
            {
                StartCoroutine(SpawnRoutine());
                yaSpawnearon = true;
            }
        }
    }

    private IEnumerator SpawnRoutine()
    {
        Efectos.SetActive(true);
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < enemigosMaximos; i++)
        {
            int spawnIndex = Random.Range(0, SpawnPoints.Length);

            Instantiate(Enemy,
                SpawnPoints[spawnIndex].position,
                SpawnPoints[spawnIndex].rotation);

            enemigosActuales++;
            yield return new WaitForSeconds(0.2f); // spawn más limpio
        }

        yield return new WaitForSeconds(1f);

        Efectos.SetActive(false);
    }
}
