using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarneArcadeMinigame : MonoBehaviour
{
    ArcadeMinigamePlayer player;

    void Start()
    {
        player = FindObjectOfType<ArcadeMinigamePlayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.CogeCarne();
            player.puntuacion += 5;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }

}
