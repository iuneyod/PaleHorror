using System.Collections;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeGame : MonoBehaviour
{
    public ArcadeLight[] luces;
    public float tiempoEncendida = 2f;
    public float tiempoMinimo = 1f;
    public float reduccionPorAcierto = 0.1f;
    public int fallosMax = 3;

    public TMP_Text score;
    public TMP_Text fallosText;

    public int puntos;

    int fallos = 0;
    bool jugando = true;

    public GameObject pantallaNegra;

    void Start()
    {
        StartCoroutine(Juego());
    }

    void Update()
    {
        fallosText.text = "Fallos: " + fallos + " / " + fallosMax;
        score.text = "SCORE: " + puntos;
    }

    IEnumerator Juego()
    {
        while (jugando)
        {
            ArcadeLight luz = luces[Random.Range(0, luces.Length)];
            luz.Encender();

            yield return new WaitForSeconds(tiempoEncendida);

            if (!luz.fuePulsada)
            {
                luz.Apagar();
                Fallo();
            }
        }
    }

    public void Acierto()
    {
        puntos++;
        Debug.Log("ACIERTO");

        tiempoEncendida = Mathf.Max(
            tiempoMinimo,
            tiempoEncendida - reduccionPorAcierto
        );
    }

    public void Fallo()
    {
        fallos++;
        Debug.Log("FALLO");

        if (fallos >= fallosMax)
        {
            jugando = false;
            StartCoroutine(FadeInPantallaNegra(2f));
        }
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
        SceneManager.LoadScene("MainScene");
    }
}
