using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject pantallaNegra;
    public Button startButton;
    public Button ExitButton;

    public GameObject menuDatos;

    private void Start()
    {
        Time.timeScale = 1f;

        CanvasGroup cg = pantallaNegra.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = pantallaNegra.AddComponent<CanvasGroup>();

        cg.alpha = 1f;
        pantallaNegra.SetActive(true);

        StartCoroutine(FadeOutPantallaNegra(2f));

        Cursor.lockState = CursorLockMode.None;
    }

    public void GoMenu()
    {
        StartCoroutine(FadeInPantallaNegra(2f));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BorrarDatos()
    {
        GameManager.Instance.ResetearYReiniciar();
        menuDatos.SetActive(false);
    }


    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
    }


    public void AbrirMenuDatos()
    {
        menuDatos.SetActive(true);
    }

    public void CerrarMenuDatos()
    {
        menuDatos.SetActive(false);
    }

    private IEnumerator FadeInPantallaNegra(float duration)
    {
        Time.timeScale = 1f;

        pantallaNegra.SetActive(true);
        CanvasGroup canvasGroup = pantallaNegra.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = pantallaNegra.AddComponent<CanvasGroup>();

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = elapsed / duration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        yield return new WaitForSecondsRealtime(0.3f);

        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator FadeOutPantallaNegra(float duration)
    {
        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CanvasGroup canvasGroup = pantallaNegra.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = pantallaNegra.AddComponent<CanvasGroup>();
        }
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        pantallaNegra.SetActive(false);
    }
}
