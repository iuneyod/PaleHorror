using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;

public class ArcadeMinigamePlayer : MonoBehaviour
{
    public float velocidad = 6f;
    public int puntuacion = 0;

    public TMP_Text puntuacionText;
    public TMP_Text puntuacionTotal;
    public Button reiniciarButton;
    public Button volverAlLobby;
    public TMP_Text gameName;
    public Button StartPlay;
    public bool enJuego = false;

    private Rigidbody2D rb;
    public  bool derrota = false;

    void Start()
    {
        Time.timeScale = 1f; // MUY IMPORTANTE
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        puntuacionTotal.gameObject.SetActive(false);
        reiniciarButton.gameObject.SetActive(false);
        volverAlLobby.gameObject.SetActive(false);
        gameName.gameObject.SetActive(true);
        StartPlay.gameObject.SetActive(true);
    }

    void Update()
    {
        puntuacionText.text = "Puntuación: " + puntuacion;

        if (derrota)
            return;

        if(puntuacion >= 500)
        {
            if(GameManager.Instance.LogroDesbloqueado(LogrosID.PuntuacionPeq) == false)
            {
                GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.PuntuacionPeq);
                GameManager.Instance.MostrarAvisoLogro(LogrosID.PuntuacionPeq, "UN PEQUEÑO RETO");
                GameManager.Instance.PuntuacionPeq = true;
                GameManager.Instance.Guardar();
            }
        }
        if (puntuacion >= 1000)
        {
            if (GameManager.Instance.LogroDesbloqueado(LogrosID.PuntuacionGrande) == false)
            {
                GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.PuntuacionGrande);
                GameManager.Instance.MostrarAvisoLogro(LogrosID.PuntuacionGrande, "DINOEXPERTO");
                GameManager.Instance.PuntuacionGrande = true;
                GameManager.Instance.Guardar();
            }
        }

    }

    void FixedUpdate()
    {
        if (derrota || !enJuego)
            return;

        float movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !derrota)
        {
            ActivarDerrota();
        }
    }

    void ActivarDerrota()
    {
        derrota = true;
        enJuego = false;   // detiene spawns
        rb.velocity = Vector2.zero;

        Time.timeScale = 0f;

        puntuacionTotal.gameObject.SetActive(true);
        puntuacionTotal.text = "Puntuación Total: " + puntuacion;

        reiniciarButton.gameObject.SetActive(true);
        volverAlLobby.gameObject.SetActive(true);
    }


    public void VolverAJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverAlLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void StartGame()
    {
        gameName.gameObject.SetActive(false);
        StartPlay.gameObject.SetActive(false);
        enJuego = true;
    }

}
