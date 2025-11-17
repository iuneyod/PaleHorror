using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform[] spawnPoints;
    public bool inArea = false;
    public int HitsNeeded = 8;

    Hammer1 hammer1;

    private void Start()
    {
        hammer1 = FindObjectOfType<Hammer1>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
        }

        if (other.CompareTag("PlayerPunch") && hammer1.isCarryingHammer == true)
        {
            HitsNeeded -= 1;
            if (HitsNeeded <= 0)
            {
                Instantiate(rockPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                HitsNeeded = 8;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}

