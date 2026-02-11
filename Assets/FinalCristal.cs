using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinalCristal : ObjetoInteractivo
{
    JukeBox juke;
    [Header("Cinemática")]
    public PlayableDirector timeline;
    public GameObject cameraa;
    Selected SELECTED;

    public bool yaActivado = false;
    public GameObject pantallaNegra;
    public GameObject efects;

    public AudioSource music;

    private void Start()
    {
        cameraa.SetActive(false);
        SELECTED = FindAnyObjectByType<Selected>();
        juke = FindAnyObjectByType<JukeBox>();
    }


    public override void ActivarObjeto()
    {
        if (yaActivado) return;
        yaActivado = true;
        GameManager.Instance.enCinematica = true;

        juke.PararMusica();
        music.Play();
        cameraa.SetActive(true);
        GameManager.Instance.cristalObtenido = true;
        SELECTED.sistemaActivo = false;

        if (timeline != null)
        {
            timeline.time = 0;
            timeline.Play();
            StartCoroutine(FinalGame());
        }
        else
        {
            Debug.LogError("PlayableDirector no asignado");
        }

    }

    IEnumerator FinalGame()
    {
        yield return new WaitForSeconds(3f);
        efects.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Credits");
    }
}
