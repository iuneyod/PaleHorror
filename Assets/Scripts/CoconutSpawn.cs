using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject coconutPrefab;


    void Start()
    {
        
            PosibilidadDeSpawn();
        
    }

    public void PosibilidadDeSpawn()
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < 80) // % chance to spawn a coconut
        {
            Instantiate(coconutPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
