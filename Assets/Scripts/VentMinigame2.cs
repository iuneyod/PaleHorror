using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentMinigame2 : ObjetoInteractivo
{
    public GameObject PantallaNegra;
    Minijuego2 minijuego2;

    public override void ActivarObjeto()
    {
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
