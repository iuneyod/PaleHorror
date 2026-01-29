using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorArcadeMinigame : MonoBehaviour
{
    ArcadeMinigamePlayer player;

    void Start()
    {
        player = FindObjectOfType<ArcadeMinigamePlayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
