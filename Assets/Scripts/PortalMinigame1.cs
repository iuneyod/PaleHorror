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
    public BoxCollider Portal2Collider;
    public BoxCollider Portal3Collider;
    public BoxCollider Portal4Collider;
    public Transform Portal4Transform;
    public GameObject Portal4Entero;


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
            Portal2Collider.enabled = false;
            Portal3Collider.enabled = false;
            Portal4Collider.enabled = false;
            GameManager.Instance.Guardar();
        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == false && GameManager.Instance.Minigame3Completado == false && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(false);
            Portal4.gameObject.SetActive(false);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = false;
            Portal4Collider.enabled = false;

            GameManager.Instance.Guardar();

        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == true && GameManager.Instance.Minigame3Completado == false && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal4.gameObject.SetActive(false);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = true;
            Portal4Collider.enabled = false;
            GameManager.Instance.Guardar();

        }
        if (GameManager.Instance.Minigame1Completado == true && GameManager.Instance.Minigame2Completado == true && GameManager.Instance.Minigame3Completado == true && GameManager.Instance.Minigame4Completado == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal4.gameObject.SetActive(true);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = true;
            Portal4Collider.enabled = true;
            GameManager.Instance.Guardar();

        }
        if (GameManager.Instance.CompletoTodosLosMinijuegos == true)
        {
            Portal4.gameObject.SetActive(false);
            Portal4Collider.enabled = false;
            Portal4Entero.transform.position = Portal4Transform.position;
            Portal4Entero.transform.rotation = Portal4Transform.rotation;
        }
        if (GameManager.Instance.CompletoTodosLosMinijuegos == true && GameManager.Instance.Minigame1Dificil == false && GameManager.Instance.Minigame2Dificil == false && GameManager.Instance.Minigame3Dificil == false)
        {
            Portal2.gameObject.SetActive(false);
            Portal3.gameObject.SetActive(false);
            Portal2Collider.enabled = false;
            Portal3Collider.enabled = false;
        }
        if (GameManager.Instance.CompletoTodosLosMinijuegos == true && GameManager.Instance.Minigame1Dificil == true && GameManager.Instance.Minigame2Dificil == false && GameManager.Instance.Minigame3Dificil == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(false);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = false;
        }
        if (GameManager.Instance.CompletoTodosLosMinijuegos == true && GameManager.Instance.Minigame1Dificil == true && GameManager.Instance.Minigame2Dificil == true && GameManager.Instance.Minigame3Dificil == false)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = true;
        }
        if (GameManager.Instance.CompletoTodosLosMinijuegos == true && GameManager.Instance.Minigame1Dificil == true && GameManager.Instance.Minigame2Dificil == true && GameManager.Instance.Minigame3Dificil == true)
        {
            Portal2.gameObject.SetActive(true);
            Portal3.gameObject.SetActive(true);
            Portal2Collider.enabled = true;
            Portal3Collider.enabled = true;
        }

    }
}
