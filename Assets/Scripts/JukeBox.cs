using System.Collections.Generic;
using UnityEngine;

public class JukeBox : ObjetoInteractivo
{
    [Header("Música")]
    public List<AudioClip> music;

    [Header("Audio")]
    public AudioSource audioSource;

    int ultimoIndice = -1;

    public override void ActivarObjeto()
    {
        if (music == null || music.Count == 0)
            return;

        // Para la música actual (si hay)
        audioSource.Stop();

        int nuevoIndice = ObtenerIndiceAleatorio();

        AudioClip clip = music[nuevoIndice];
        audioSource.clip = clip;
        audioSource.Play();

        ultimoIndice = nuevoIndice;
    }

    int ObtenerIndiceAleatorio()
    {
        if (music.Count == 1)
            return 0;

        int index;
        do
        {
            index = Random.Range(0, music.Count);
        }
        while (index == ultimoIndice);

        return index;
    }
}
