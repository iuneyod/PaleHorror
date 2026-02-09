using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [Header("Movimiento")]
    public RectTransform creditsContainer;
    public float scrollSpeed = 50f;

    [Header("Escenas")]
    public string escenaSalir = "MainMenu";

    private bool salir = false;

    private bool puedeSalir = false;
    public TMP_Text textoSalir;

    public AudioSource MUSIC;

    private void Start()
    {
        textoSalir.gameObject.SetActive(false);
        StartCoroutine(Salir());
        MUSIC.Play();
    }

    void Update()
    {
        // Scroll hacia arriba
        creditsContainer.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // Salir con SPACE
        if (Input.GetKeyDown(KeyCode.Space) && puedeSalir == true)
        {
            SalirCreditos();
        }

        if (creditsContainer.anchoredPosition.y > 2405f)
        {
            SalirCreditos();
        }

        if (GameManager.Instance.cristalObtenido == true)
        {
            GameManager.Instance.ResetearProgreso();
        }

    }

    void SalirCreditos()
    {
        if (salir) return;
        salir = true;

        SceneManager.LoadScene(escenaSalir);
    }

    IEnumerator Salir()
    {
        yield return new WaitForSeconds(15f);
        puedeSalir = true;
        textoSalir.gameObject.SetActive(true);
    }
}
