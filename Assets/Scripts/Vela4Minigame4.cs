using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vela4Minigame4 : MonoBehaviour
{
    AcertijoCoche1 acertijoCoche1;
    AcertijoCoche2 acertijoCoche2;
    AcertijoCoche3 acertijoCoche3;
    AcertijoCoche4 acertijoCoche4;
    public GameObject fight4;
    public bool vela4Encendida = false;

    void Start()
    {
        acertijoCoche1 = FindObjectOfType<AcertijoCoche1>();
        acertijoCoche2 = FindObjectOfType<AcertijoCoche2>();
        acertijoCoche3 = FindObjectOfType<AcertijoCoche3>();
        acertijoCoche4 = FindObjectOfType<AcertijoCoche4>();
    }


    public void Update()
    {
        if (acertijoCoche1.mirandoDerecha == true && acertijoCoche2.mirandoDerecha == true && acertijoCoche3.mirandoIzquierda == true && acertijoCoche4.mirandoIzquierda == true)
        {
            vela4Encendida = true;
            fight4.SetActive(true);
        }
        else
        {
            vela4Encendida = false;
            fight4.SetActive(false);
        }
    }
}
