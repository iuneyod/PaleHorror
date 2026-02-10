using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverClickTV_Menu : MonoBehaviour
{
    public GameObject pantallaNegra;
    public CanvasGroup estatica;
    public CanvasGroup imagenMenu;

    public float tiempoDesvanecer = 0.5f;

    private bool hover;
    private bool estabaHover;

    private Coroutine transicionActual;

    void Start()
    {
        estatica.alpha = 1f;
        imagenMenu.alpha = 0f;

        imagenMenu.gameObject.SetActive(true);
        estatica.gameObject.SetActive(true);
    }

    void Update()
    {
        DetectarHover();

        if (hover && !estabaHover)
            IniciarTransicion(false);

        if (!hover && estabaHover)
            IniciarTransicion(true);

        estabaHover = hover;

        DetectarClick();
    }

    void IniciarTransicion(bool mostrarEstatica)
    {
        if (transicionActual != null)
            StopCoroutine(transicionActual);

        transicionActual = StartCoroutine(TransicionEstatica(mostrarEstatica));
    }

    void DetectarHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        hover = Physics.Raycast(ray, out hit) && hit.transform == transform;
    }

    IEnumerator TransicionEstatica(bool mostrarEstatica)
    {
        float tiempo = 0f;

        float inicioEstatica = estatica.alpha;
        float finEstatica = mostrarEstatica ? 1f : 0f;

        float inicioMenu = imagenMenu.alpha;
        float finMenu = mostrarEstatica ? 0f : 0.75f;

        while (tiempo < tiempoDesvanecer)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / tiempoDesvanecer;

            estatica.alpha = Mathf.Lerp(inicioEstatica, finEstatica, t);
            imagenMenu.alpha = Mathf.Lerp(inicioMenu, finMenu, t);

            yield return null;
        }

        estatica.alpha = finEstatica;
        imagenMenu.alpha = finMenu;
    }

    void DetectarClick()
    {
        if (hover && Input.GetMouseButtonDown(0))
            StartCoroutine(FadeInPantallaNegra(2f));
    }

    IEnumerator FadeInPantallaNegra(float duration)
    {
        pantallaNegra.SetActive(true);

        CanvasGroup canvasGroup = pantallaNegra.GetComponent<CanvasGroup>();
        if (!canvasGroup)
            canvasGroup = pantallaNegra.AddComponent<CanvasGroup>();

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = elapsed / duration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
