using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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
    public int latasComidas = 0;
    public bool tieneObjetoArcade1 = false;
    public bool tieneObjetoArcade2 = false;
    public bool yaCayoPaleMadera = false;

    public bool CompletoTodosLosMinijuegos = false;

    public bool Minigame1Completado = false;
    public bool Minigame2Completado = false;
    public bool Minigame3Completado = false;
    public bool Minigame4Completado = false;

    public bool Minigame1Dificil = false;
    public bool Minigame2Dificil = false;
    public bool Minigame3Dificil = false;
    public bool Minigame4Dificil = false;


    public void ComerLata()
    {
        latasComidas++;
        Guardar();

        if (latasComidas >= 10)
        {
            DesbloquearLogro(LogrosID.LATAS_10);
        }
    }


    public void ObjetosArcade()
    {
        if (tieneObjetoArcade1 == true && tieneObjetoArcade2 == true)
        {
            DesbloquearLogro(LogrosID.DosObjetosArcade);
        }
    }

    public void ObtenerObjetoArcade1()
    {
        tieneObjetoArcade1 = true;
        Guardar();
        ObjetosArcade();
    }

    public void ObtenerObjetoArcade2()
    {
        tieneObjetoArcade2 = true;
        Guardar();
        ObjetosArcade();
    }


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

        Cargar();
    }

    /* =========================
     *  MINIJUEGOS
     * ========================= */

    public static class LogrosID
    {
        public const string PRIMER_MINIJUEGO = "PRIMER_MINIJUEGO";
        public const string LATAS_10 = "LATAS_10";
        public const string Canasta_5 = "Canasta_5";
        public const string Todos_Los_Minijuegos = "Todos_Los_Minijuegos";
        public const string DosObjetosArcade = "DosObjetosArcade";
        public const string Minijuego1_Dificil = "Minijuego1_Dificil";
        public const string Minijuego2_Dificil = "Minijuego2_Dificil";
    }


    public void CompletarMinijuego(string id)
    {
        if (!minijuegosCompletados.Contains(id))
        {
            minijuegosCompletados.Add(id);
            Guardar();
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
            Guardar();
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
            Guardar();
        }
    }

    public bool FlagActivo(string id)
    {
        return flagsHistoria.Contains(id);
    }

    /* =========================
     *  GUARDADO SIMPLE
     * ========================= */

    public void Guardar()
    {
        PlayerPrefs.SetString(
            "Minijuegos",
            string.Join(",", minijuegosCompletados)
        );

        PlayerPrefs.SetString(
            "Logros",
            string.Join(",", logrosDesbloqueados)
        );

        PlayerPrefs.SetString(
            "Flags",
            string.Join(",", flagsHistoria)
        );

        PlayerPrefs.SetInt("Rango", rango);
        PlayerPrefs.SetInt("Puntuacion", puntuacionTotal);

        PlayerPrefs.SetInt("Latas", latasComidas);
        PlayerPrefs.SetInt("ObjArcade1", tieneObjetoArcade1 ? 1 : 0);
        PlayerPrefs.SetInt("ObjArcade2", tieneObjetoArcade2 ? 1 : 0);
        PlayerPrefs.SetInt("YaCayoPaleMadera", yaCayoPaleMadera ? 1 : 0);
        PlayerPrefs.SetInt("CompletoTodosLosMinijuegos", CompletoTodosLosMinijuegos ? 1 : 0);
        PlayerPrefs.SetInt("Minigame1Completado", Minigame1Completado ? 1 : 0);
        PlayerPrefs.SetInt("Minigame2Completado", Minigame2Completado ? 1 : 0);
        PlayerPrefs.SetInt("Minigame3Completado", Minigame3Completado ? 1 : 0);
        PlayerPrefs.SetInt("Minigame4Completado", Minigame4Completado ? 1 : 0);
        PlayerPrefs.SetInt("Minigame1Dificil", Minigame1Dificil ? 1 : 0);
        PlayerPrefs.SetInt("Minigame2Dificil", Minigame2Dificil ? 1 : 0);
        PlayerPrefs.SetInt("Minigame3Dificil", Minigame3Dificil ? 1 : 0);
        PlayerPrefs.SetInt("Minigame4Dificil", Minigame4Dificil ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void Cargar()
    {
        if (PlayerPrefs.HasKey("Minijuegos"))
        {
            var data = PlayerPrefs.GetString("Minijuegos");
            if (!string.IsNullOrEmpty(data))
                minijuegosCompletados = new HashSet<string>(data.Split(','));
        }

        if (PlayerPrefs.HasKey("Logros"))
        {
            var data = PlayerPrefs.GetString("Logros");
            if (!string.IsNullOrEmpty(data))
                logrosDesbloqueados = new HashSet<string>(data.Split(','));
        }

        if (PlayerPrefs.HasKey("Flags"))
        {
            var data = PlayerPrefs.GetString("Flags");
            if (!string.IsNullOrEmpty(data))
                flagsHistoria = new HashSet<string>(data.Split(','));
        }

        rango = PlayerPrefs.GetInt("Rango", 0);
        puntuacionTotal = PlayerPrefs.GetInt("Puntuacion", 0);

        latasComidas = PlayerPrefs.GetInt("Latas", 0);
        tieneObjetoArcade1 = PlayerPrefs.GetInt("ObjArcade1", 0) == 1;
        tieneObjetoArcade2 = PlayerPrefs.GetInt("ObjArcade2", 0) == 1;
        yaCayoPaleMadera = PlayerPrefs.GetInt("YaCayoPaleMadera", 0) == 1;
        CompletoTodosLosMinijuegos = PlayerPrefs.GetInt("CompletoTodosLosMinijuegos", 0) == 1;
        Minigame1Completado = PlayerPrefs.GetInt("Minigame1Completado", 0) == 1;
        Minigame2Completado = PlayerPrefs.GetInt("Minigame2Completado", 0) == 1;
        Minigame3Completado = PlayerPrefs.GetInt("Minigame3Completado", 0) == 1;
        Minigame4Completado = PlayerPrefs.GetInt("Minigame4Completado", 0) == 1;
        Minigame1Dificil = PlayerPrefs.GetInt("Minigame1Dificil", 0) == 1;
        Minigame2Dificil = PlayerPrefs.GetInt("Minigame2Dificil", 0) == 1;
        Minigame3Dificil = PlayerPrefs.GetInt("Minigame3Dificil", 0) == 1;
        Minigame4Dificil = PlayerPrefs.GetInt("Minigame4Dificil", 0) == 1;
        ObjetosArcade();
    }

    /* =========================
     *  UTILIDAD
     * ========================= */

    public void ResetearProgreso()
    {
        minijuegosCompletados.Clear();
        logrosDesbloqueados.Clear();
        flagsHistoria.Clear();
        latasComidas = 0;
        tieneObjetoArcade1 = false;
        tieneObjetoArcade2 = false;
        yaCayoPaleMadera = false;
        CompletoTodosLosMinijuegos = false;
        Minigame1Completado = false;
        Minigame2Completado = false;
        Minigame3Completado = false;
        Minigame4Completado = false;
        Minigame1Dificil = false;
        Minigame2Dificil = false;
        Minigame3Dificil = false;
        Minigame4Dificil = false;



        rango = 0;
        puntuacionTotal = 0;

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ResetearYReiniciar()
    {
        ResetearProgreso();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
