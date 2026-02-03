using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSalida : MonoBehaviour
{
    public string nombreScene;
    public GameObject pantallaNegra;
    public bool minijuego3Completado = false;

    public void OnTriggerEnter(Collider other)
    {
        minijuego3Completado = true;
        GameManager.Instance.CompletarMinijuego("MINIJUEGO3");
        GameManager.Instance.Minigame3Completado = true;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CambiarScene());
            this.transform.position = new Vector3(0, +100, 0);
        }
        GameManager.Instance.Guardar();
    }

    IEnumerator CambiarScene()
    {
        yield return FadeInPantallaNegra(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nombreScene);
    }


    private IEnumerator FadeInPantallaNegra(float duration)
    {
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
    }
}
