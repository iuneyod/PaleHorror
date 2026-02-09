using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMinigame1 : ObjetoInteractivo
{
    public GameObject cameraPlayer;
    public Transform posicionInicialCamera;
    public Transform posicionFinalCamera;
    public GameObject pantallaNegra;
    public string nombreScene;
    public bool puedeActivar = true;
    public AudioSource sonidoPortal;

    public GameObject Portal2;
    public GameObject Portal3;
    public GameObject Portal4;

    private void Start()
    {
        sonidoPortal = GetComponent<AudioSource>();
    }


    public override void ActivarObjeto()
    {
        if (!puedeActivar) return;
        StartCoroutine(AcercarCamaraPortal());
        StartCoroutine(FadeInPantallaNegra(2f));
        sonidoPortal.Play();
    }

    private IEnumerator FadeInPantallaNegra(float duration)
    {
        puedeActivar = false;
        pantallaNegra.SetActive(true);
        CanvasGroup canvasGroup = pantallaNegra.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = pantallaNegra.AddComponent<CanvasGroup>();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreScene);
        puedeActivar = true;
    }

    IEnumerator AcercarCamaraPortal()
    {
        //quitar el parent
        cameraPlayer.transform.parent = null;
        cameraPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            cameraPlayer.transform.position = Vector3.Lerp(posicionInicialCamera.position, posicionFinalCamera.position, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraPlayer.transform.position = posicionFinalCamera.position;
    }

    public void Portales()
    {
        if (GameManager.Instance.Minigame1Completado == false && GameManager.Instance.Minigame2Completado == false && GameManager.Instance.Minigame3Completado == false && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(false);
            Portal3.gameObject.SetActive(false);
            Portal4.gameObject.SetActive(false);
            GameManager.Instance.Guardar();
        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == false && GameManager.Instance.Minigame3Completado == false && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(false);
            Portal4.gameObject.SetActive(false);
            GameManager.Instance.Guardar();

        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == true && GameManager.Instance.Minigame3Completado == false && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal4.gameObject.SetActive(false);
            GameManager.Instance.Guardar();

        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == true && GameManager.Instance.Minigame3Completado == true && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal4.gameObject.SetActive(true);
            GameManager.Instance.Guardar();

        }
    }
}
