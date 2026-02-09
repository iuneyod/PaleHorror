using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vela3Minigame4 : MonoBehaviour
{
    EagleMinigame4 eagleMinigame4;
    RabbitMinigame4 rabbitMinigame4;
    public bool vela3Encendida = false;
    public GameObject fire3;

    public AudioSource vela;

    private void Start()
    {
        eagleMinigame4 = FindObjectOfType<EagleMinigame4>();
        rabbitMinigame4 = FindObjectOfType<RabbitMinigame4>();
    }

    public void Update()
    {
        if (eagleMinigame4.mirandoAtras == true && rabbitMinigame4.mirandoDerecha == true)
        {
            vela.Play();
            vela3Encendida = true;
            fire3.SetActive(true);
        }
        else
        {
            vela3Encendida = false;
            fire3.SetActive(false);
        }
    }
}
