using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentMinigame2 : ObjetoInteractivo
{
    public GameObject PantallaNegra;
    Minijuego2 minijuego2;
    public bool minijuego2Completado = false;
    public bool minigame2DificilCompletado = false;

    public AudioSource entrar;



    private void Start()
    {
        minijuego2 = FindObjectOfType<Minijuego2>();
    }
    public override void ActivarObjeto()
    {
        entrar.Play();
        GameManager.Instance.CompletarMinijuego("MINIJUEGO2");
        GameManager.Instance.Minigame2Completado = true;

        GameManager.Instance.ComprobarTodosLosMinijuegos();


        if (GameManager.Instance.MiniguegosCompletadosCount() >= 4)
        {
            GameManager.Instance.Minigame2Dificil = true;
            GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.Minijuego2_Dificil);
        }

        GameManager.Instance.JuegoTerminado();
        GameManager.Instance.Guardar();

        minijuego2.puedeAtacar = false;

        StartCoroutine(FadeYcargar());
    }


    IEnumerator FadeYcargar()
    {
        yield return StartCoroutine(FadeInPantallaNegra(1.5f));
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator FadeInPantallaNegra(float duration)
    {
        PantallaNegra.SetActive(true);
        CanvasGroup canvasGroup = PantallaNegra.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = PantallaNegra.AddComponent<CanvasGroup>();
        }
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(0.5f);
    }


}
