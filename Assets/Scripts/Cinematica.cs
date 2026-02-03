using System.Collections;
using UnityEngine;

public class Cinematica : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource personaje1;
    public AudioSource personaje2;

    [Header("Diálogos")]
    public AudioClip[] dialogosP1;
    public AudioClip[] dialogosP2;

    void Start()
    {
        // Voz más grave para P2
        personaje2.pitch = 0.8f;
        StartCoroutine(Dialogo_Acto1());
    }

    IEnumerator Dialogo_Acto1()
    {
        int total = Mathf.Min(dialogosP1.Length, dialogosP2.Length);

        for (int i = 0; i < total; i++)
        {
            // Personaje 1
            yield return ReproducirLinea(personaje1, dialogosP1[i]);

            // Personaje 2
            yield return ReproducirLinea(personaje2, dialogosP2[i]);
        }

        Debug.Log("Diálogo terminado");
    }

    IEnumerator ReproducirLinea(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();

        // Espera hasta que termine de sonar el audio
        while (source.isPlaying)
        {
            yield return null; // espera 1 frame
        }
    }
}