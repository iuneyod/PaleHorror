using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerArcade : MonoBehaviour
{
    public ArcadeMinigamePlayer player;
    public List<Transform> spawnPoints;
    public GameObject carnePrefab;
    public GameObject meteoritoPrefab;

    public float tiempoEntreSpawns = 0.6f;
    private float valueRandom = 0.35f;

    private Coroutine spawnCoroutine;

    void Update()
    {
        if (player.enJuego && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }

        if (!player.enJuego && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        // Dificultad dinámica
        if (player.puntuacion >= 600)
        {
            valueRandom = 0.2f;
            tiempoEntreSpawns = 0.25f;
        }
        else if (player.puntuacion >= 300)
        {
            valueRandom = 0.25f;
            tiempoEntreSpawns = 0.3f;
        }
        else if (player.puntuacion >= 100)
        {
            valueRandom = 0.30f;
            tiempoEntreSpawns = 0.4f;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Count)];

            GameObject prefab = Random.value < valueRandom ? carnePrefab : meteoritoPrefab;

            Instantiate(prefab, spawn.position, Quaternion.identity);

            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
}
