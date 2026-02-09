using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

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

        if (puntos >= 50)
        {
            if (GameManager.Instance.LogroDesbloqueado(LogrosID.Puntuacion2Arcade) == false)
            {
                GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.Puntuacion2Arcade);
                GameManager.Instance.MostrarAvisoLogro(LogrosID.Puntuacion2Arcade, "AMANTE DE LOS RECREATIVOS");
                GameManager.Instance.Puntuacion2Arcade = true;
                GameManager.Instance.Guardar();
            }
        }
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
