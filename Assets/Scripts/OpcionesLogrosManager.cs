using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;

public class OpcionesLogrosManager : MonoBehaviour
{
    PortalMinigame1 portal;
    ArcadeMachine arcade;
    Selected selectedScript;
    public bool puedeAbrirUI = true;
    public GameObject UI;
    public GameObject logrosUI;
    public GameObject menuUI;


    public bool UIAbiertos = false;
    public GameObject TickCompletado_PrimerMinijuego;
    public GameObject TickCompletado_Comer10latas;
    public GameObject TickCompletado_TodosLosMinijuegos;
    public GameObject TickCompletado_5canastas;
    public GameObject TickCompletado_JuegosArcade;
    public GameObject TickCompletado_MinijuegoDificil1;
    public GameObject TickCompletado_MinijuegoDificil2;
    public GameObject TickCompletado_TodosMinijuegosDificiles;
    public GameObject TickCompletado_500Puntos;
    public GameObject TickCompletado_1000Puntos;
    public GameObject TickCompletado_20PuntosArcade;
    public GameObject TickCompletado_EscuchaMusica;

    public Button botonMenu;
    public Button botonLogros;

    public GameObject LightMenu;

    public AudioSource music;
    public GameObject cristal;
    public bool yaAparecio = false;

    private void Start()
    {
        portal = FindAnyObjectByType<PortalMinigame1>();
        portal.Portales();
        music.Play();
        UI.SetActive(false);
        selectedScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Selected>();
        arcade = FindAnyObjectByType<ArcadeMachine>();

        if (GameManager.Instance.Minigame1Dificil &&
        !GameManager.Instance.avisosLogrosMostrados.Contains(LogrosID.Minijuego1_Dificil))
        {
            GameManager.Instance.MostrarAvisoLogro(
                LogrosID.Minijuego1_Dificil,
                "OTRA VEZ UNA PRIMERA VEZ"
            );
        }
        GameManager.Instance.MostrarAvisosPendientes();

    }

    private void Update()
    {
        if (GameManager.Instance.puedeAparecerCristal == true && yaAparecio == false)
        {
            yaAparecio = true;
            cristal.gameObject.SetActive(true);
        }

        if (GameManager.Instance.CompletoTodosLosMinijuegos == true)
        {
            LightMenu.GetComponent<Light>().color = Color.red;
            LightMenu.GetComponent <Light>().intensity = 0.6f;
            GameManager.Instance.Guardar();
        }
        else
        {
            LightMenu.GetComponent<Light>().color = Color.white;
            LightMenu.GetComponent<Light>().intensity = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)
         && puedeAbrirUI
         && arcade.pantallaAbierta == false
         && arcade.acabaDeCerrarArcade == false)
        {
            UIAbiertos = !UIAbiertos;
            UI.SetActive(UIAbiertos);
            logrosUI.SetActive(false);
            menuUI.SetActive(true);
            ActualizarTicksLogros();
        }

        if (UI.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
            botonMenu.interactable = true;
            botonLogros.interactable = true;
            selectedScript.sistemaActivo = false;
        }

        if (UIAbiertos == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (GameManager.Instance.MinijuegoCompletado("MINIJUEGO1") == true && GameManager.Instance.MinijuegoCompletado("MINIJUEGO2") == true &&
            GameManager.Instance.MinijuegoCompletado("MINIJUEGO3") == true && GameManager.Instance.MinijuegoCompletado("MINIJUEGO4") == true)
        {
            GameManager.Instance.DesbloquearLogro(GameManager.LogrosID.Todos_Los_Minijuegos);
            GameManager.Instance.CompletoTodosLosMinijuegos = true;
        }

    }

    public void BotonLogros()
    {
        logrosUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void BotonMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ActualizarTicksLogros()
    {
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.PRIMER_MINIJUEGO) == true)
        {
            TickCompletado_PrimerMinijuego.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.LATAS_10) == true)
        {
            TickCompletado_Comer10latas.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.Todos_Los_Minijuegos) == true)
        {
            TickCompletado_TodosLosMinijuegos.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.Canasta_5) == true)
        {
            TickCompletado_5canastas.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.DosObjetosArcade) == true)
        {
            TickCompletado_JuegosArcade.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.Minijuego1_Dificil) == true)
        {
            TickCompletado_MinijuegoDificil1.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.Minijuego2_Dificil) == true)
        {
            TickCompletado_MinijuegoDificil2.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.JuegoCompletado) == true)
        {
            TickCompletado_TodosMinijuegosDificiles.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.PuntuacionPeq) == true)
        {
            TickCompletado_500Puntos.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.PuntuacionGrande) == true)
        {
            TickCompletado_1000Puntos.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.Puntuacion2Arcade) == true)
        {
            TickCompletado_20PuntosArcade.SetActive(true);
        }
        if (GameManager.Instance.LogroDesbloqueado(GameManager.LogrosID.EscuchoMusica) == true)
        {
            TickCompletado_EscuchaMusica.SetActive(true);
        }

    }
}
