using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Escenas")]
    [Tooltip("Nombre de la escena principal (hub)")]
    public string escenaHub = "MainHub";

    [Header("Progreso")]
    public HashSet<string> minijuegosCompletados = new();
    public HashSet<string> logrosDesbloqueados = new();
    public HashSet<string> flagsHistoria = new();

    [Header("Datos del jugador")]
    public int rango = 0;
    public int puntuacionTotal = 0;
    public int comidaTotalComida = 0;

    void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Cargar();
    }

    /* =========================
     *  MINIJUEGOS
     * ========================= */

    public void CompletarMinijuego(string id)
    {
        if (!minijuegosCompletados.Contains(id))
        {
            minijuegosCompletados.Add(id);
            //Guardar();
        }
    }

    public bool MinijuegoCompletado(string id)
    {
        return minijuegosCompletados.Contains(id);
    }

    /* =========================
     *  LOGROS
     * ========================= */

    public void DesbloquearLogro(string id)
    {
        if (!logrosDesbloqueados.Contains(id))
        {
            logrosDesbloqueados.Add(id);
            //Guardar();
        }
    }

    public bool LogroDesbloqueado(string id)
    {
        return logrosDesbloqueados.Contains(id);
    }

    /* =========================
     *  HISTORIA / FLAGS
     * ========================= */

    public void ActivarFlag(string id)
    {
        if (!flagsHistoria.Contains(id))
        {
            flagsHistoria.Add(id);
            //Guardar();
        }
    }

    public bool FlagActivo(string id)
    {
        return flagsHistoria.Contains(id);
    }

    /* =========================
     *  GUARDADO SIMPLE
     * ========================= */

    //void Guardar()
    //{
    //    PlayerPrefs.SetString(
    //        "Minijuegos",
    //        string.Join(",", minijuegosCompletados)
    //    );

    //    PlayerPrefs.SetString(
    //        "Logros",
    //        string.Join(",", logrosDesbloqueados)
    //    );

    //    PlayerPrefs.SetString(
    //        "Flags",
    //        string.Join(",", flagsHistoria)
    //    );

    //    PlayerPrefs.SetInt("Rango", rango);
    //    PlayerPrefs.SetInt("Puntuacion", puntuacionTotal);

    //    PlayerPrefs.Save();
    //}

    //void Cargar()
    //{
    //    if (PlayerPrefs.HasKey("Minijuegos"))
    //    {
    //        minijuegosCompletados = new HashSet<string>(
    //            PlayerPrefs.GetString("Minijuegos").Split(',')
    //        );
    //    }

    //    if (PlayerPrefs.HasKey("Logros"))
    //    {
    //        logrosDesbloqueados = new HashSet<string>(
    //            PlayerPrefs.GetString("Logros").Split(',')
    //        );
    //    }

    //    if (PlayerPrefs.HasKey("Flags"))
    //    {
    //        flagsHistoria = new HashSet<string>(
    //            PlayerPrefs.GetString("Flags").Split(',')
    //        );
    //    }

    //    rango = PlayerPrefs.GetInt("Rango", 0);
    //    puntuacionTotal = PlayerPrefs.GetInt("Puntuacion", 0);
    //}

    /* =========================
     *  UTILIDAD
     * ========================= */

    public void ResetearProgreso()
    {
        minijuegosCompletados.Clear();
        logrosDesbloqueados.Clear();
        flagsHistoria.Clear();

        rango = 0;
        puntuacionTotal = 0;

        PlayerPrefs.DeleteAll();
    }
}
