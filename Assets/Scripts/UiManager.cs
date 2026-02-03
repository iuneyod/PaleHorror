using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [Header("Panels")]
    public GameObject optionsMenu;
    public GameObject achievementsMenu;
    public GameObject pauseMenu;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CerrarTodo();
    }

    public void AbrirOpciones()
    {
        CerrarTodo();
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AbrirLogros()
    {
        CerrarTodo();
        achievementsMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AbrirPausa()
    {
        CerrarTodo();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CerrarTodo()
    {
        optionsMenu.SetActive(false);
        achievementsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
