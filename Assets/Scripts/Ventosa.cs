using UnityEngine;
using System.Collections;

public class Ventosa : MonoBehaviour
{
    private float tiempoDesaparicion = 5f;

    private void Start()
    {
        Destroy(gameObject, tiempoDesaparicion);
    }
}
