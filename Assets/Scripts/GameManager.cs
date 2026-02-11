using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using System.Collections;

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
    public HashSet<string> avisosLogrosMostrados = new();

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

    public GameObject avisoLogro;
    public TMP_Text nameLogro;
    [Header("Aviso Logro")]
    public CanvasGroup avisoCanvas;
    public float fadeInTime = 0.35f;   // rápido
    public float visibleTime = 2.2f;
    public float fadeOutTime = 1f;   // más lento

    Coroutine avisoCoroutine;

    public AudioSource audioLogro;

    public bool puedeAparecerCristal = false;
    public bool cristalObtenido = false;
    public bool PuntuacionPeq = false;
    public bool PuntuacionGrande = false;
    public bool Puntuacion2Arcade = false;
    public bool EscuchoMusica = false;




    public void ComerLata()
    {
        latasComidas++;
        Guardar();

        if (latasComidas >= 10)
        {
            DesbloquearLogro(LogrosID.LATAS_10);
            MostrarAvisoLogro(LogrosID.LATAS_10, "GLOTÓN");
        }
    }

    public void MostrarAvisosPendientes()
    {
        if (CompletoTodosLosMinijuegos &&
            !avisosLogrosMostrados.Contains(LogrosID.Todos_Los_Minijuegos))
        {
            MostrarAvisoLogro(LogrosID.Todos_Los_Minijuegos, "¿ES EL FIN...?");
        }

        if (Minigame2Dificil == true &&
            !avisosLogrosMostrados.Contains(LogrosID.Minijuego2_Dificil))
        {
            MostrarAvisoLogro(LogrosID.Minijuego2_Dificil, "A LA SEGUNDA NO ES LA VENCIDA");
        }
    }



    public void MostrarAvisoLogro(string idLogro, string nombreVisible)
    {
        if (avisosLogrosMostrados.Contains(idLogro))
            return;

        if (avisoLogro == null || avisoCanvas == null || nameLogro == null)
        {
            return;
        }

        avisosLogrosMostrados.Add(idLogro);
        Guardar();

        if (avisoCoroutine != null)
            StopCoroutine(avisoCoroutine);

        avisoCoroutine = StartCoroutine(AvisoLogroCoroutine(nombreVisible));
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarAvisoLogroEnEscena();
    }
    void BuscarAvisoLogroEnEscena()
    {
        AvisoLogroUI ui = FindObjectOfType<AvisoLogroUI>(true);

        if (ui == null)
        {
            avisoLogro = null;
            avisoCanvas = null;
            nameLogro = null;
            audioLogro = null;
            return;
        }

        avisoLogro = ui.avisoLogro;
        avisoCanvas = ui.canvasGroup;
        nameLogro = ui.nameLogro;
        audioLogro = ui.audioSource;

        avisoLogro.SetActive(false);
        avisoCanvas.alpha = 0f;
    }


    IEnumerator AvisoLogroCoroutine(string nombreLogro)
    {
        avisoLogro.SetActive(true);
        nameLogro.text = nombreLogro;

        avisoCanvas.alpha = 0f;

        // Fade in
        float t = 0f;
        while (t < fadeInTime)
        {
            t += Time.unscaledDeltaTime;
            avisoCanvas.alpha = Mathf.Lerp(0f, 1f, t / fadeInTime);
            yield return null;
        }

        avisoCanvas.alpha = 1f;

        yield return new WaitForSecondsRealtime(visibleTime);

        // Fade out
        t = 0f;
        while (t < fadeOutTime)
        {
            t += Time.unscaledDeltaTime;
            avisoCanvas.alpha = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            yield return null;
        }

        avisoCanvas.alpha = 0f;
        avisoLogro.SetActive(false);
    }


    public void ObjetosArcade()
    {
        if (tieneObjetoArcade1 && tieneObjetoArcade2)
        {
            DesbloquearLogro(LogrosID.DosObjetosArcade);
            MostrarAvisoLogro(LogrosID.DosObjetosArcade, "COLECCIONISTA");
            Guardar();
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

    public void JuegoTerminado()
    {
        if (Minigame1Dificil == true && Minigame2Dificil == true && Minigame3Dificil == true)
        {
            if (!LogroDesbloqueado(LogrosID.JuegoCompletado))
            {
                DesbloquearLogro(LogrosID.JuegoCompletado);
                MostrarAvisoLogro(LogrosID.JuegoCompletado, "AL FIN EL FIN");
                puedeAparecerCristal = true;

                Guardar();
            }
        }
    }

    public void ComprobarTodosLosMinijuegos()
    {
        if (Minigame1Completado &&
            Minigame2Completado &&
            Minigame3Completado &&
            Minigame4Completado)
        {
            if (!CompletoTodosLosMinijuegos)
            {
                CompletoTodosLosMinijuegos = true;
                DesbloquearLogro(LogrosID.Todos_Los_Minijuegos);
                Guardar();
            }
        }
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
        public const string JuegoCompletado = "Juego_Completado";
        public const string PuntuacionPeq = "Puntuacion500";
        public const string PuntuacionGrande = "Puntuacion1000";
        public const string Puntuacion2Arcade = "Arcade2Puntos";
        public const string EscuchoMusica = "EscuchoMusica";


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
            ReproducirSonidoLogro();
            Guardar();
        }
    }

    void ReproducirSonidoLogro()
    {
        audioLogro.Play();
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

        PlayerPrefs.SetString(
            "AvisosLogrosMostrados",
            string.Join(",", avisosLogrosMostrados)
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
        PlayerPrefs.SetInt("CristalFinJuego", puedeAparecerCristal ? 1 : 0);
        PlayerPrefs.SetInt("TerminoJuego", cristalObtenido ? 1 : 0);
        PlayerPrefs.SetInt("Puntuacion500", PuntuacionPeq ? 1 : 0);
        PlayerPrefs.SetInt("Puntuacion1000", PuntuacionGrande ? 1 : 0);
        PlayerPrefs.SetInt("Arcade2Puntos", Puntuacion2Arcade ? 1 : 0);
        PlayerPrefs.SetInt("EscuchoMusica", EscuchoMusica ? 1 : 0);







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

        if (PlayerPrefs.HasKey("AvisosLogrosMostrados"))
        {
            var data = PlayerPrefs.GetString("AvisosLogrosMostrados");
            if (!string.IsNullOrEmpty(data))
                avisosLogrosMostrados = new HashSet<string>(data.Split(','));
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
        puedeAparecerCristal = PlayerPrefs.GetInt("CristalFinJuego", 0) == 1;
        cristalObtenido = PlayerPrefs.GetInt("TerminoJuego", 0) == 1;
        PuntuacionPeq = PlayerPrefs.GetInt("Puntuacion500", 0) == 1;
        PuntuacionGrande = PlayerPrefs.GetInt("Puntuacion1000", 0) == 1;
        Puntuacion2Arcade = PlayerPrefs.GetInt("Arcade2Puntos", 0) == 1;
        EscuchoMusica = PlayerPrefs.GetInt("EscuchoMusica", 0) == 1;


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
        avisosLogrosMostrados.Clear();
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
        puedeAparecerCristal = false;
        cristalObtenido = false;
        PuntuacionPeq = false;
        PuntuacionGrande = false;
        Puntuacion2Arcade = false;
        EscuchoMusica = false;



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
