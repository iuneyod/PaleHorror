using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TornilloMinigame : ObjetoInteractivo
{
    Rigidbody rb;
    public int fuerzaLanzamiento;
    Minijuego2 minijuego2;
    public AudioSource audioSource;
    public AudioClip apartar;
    public AudioClip tocar;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        minijuego2 = FindObjectOfType<Minijuego2>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void ActivarObjeto()
    {
        audioSource.PlayOneShot(apartar);
        minijuego2.hayTornillo = false;
        minijuego2.puedeAparecerTornillo = true;

        int randomY = Random.Range(0, 2);

        if (randomY == 1)
        {
            rb.AddForce(Vector3.left * fuerzaLanzamiento, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * fuerzaLanzamiento, ForceMode.Impulse);
        }

        StartCoroutine(DestruirTornillo());
    }


    IEnumerator DestruirTornillo()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(tocar);
            StartCoroutine(EsperarYAtacar());
        }
    }

    IEnumerator EsperarYAtacar()
    {
        yield return new WaitForSeconds(1f);

        if (minijuego2 == null) yield break;
        if (minijuego2.haTocadoTornillo) yield break;
        if (minijuego2.estaMuriendo) yield break;

        minijuego2.haTocadoTornillo = true;
    }

}
