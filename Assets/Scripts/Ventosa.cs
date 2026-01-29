using UnityEngine;
using System.Collections;

public class Ventosa : MonoBehaviour
{
    private float tiempoDesaparicion = 5f;
    private Rigidbody rb;
    private bool yaPegado = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, tiempoDesaparicion);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (yaPegado) return;
        if (other.isTrigger) return;

        transform.position = other.ClosestPoint(transform.position);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.SetParent(other.transform);
        yaPegado = true;
    }
}
