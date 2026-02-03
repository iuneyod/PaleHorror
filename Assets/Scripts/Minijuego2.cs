using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minijuego2 : MonoBehaviour
{
    public GameObject pantallaNegra;

    public GameObject player;
    public GameObject cameraPlayer;
    public Transform cameraTransform;

    //Tornillo
    public GameObject Tornillo;
    public bool haTocadoTornillo = false;
    public Transform puntoTornillo;
    public bool hayTornillo = false;
    public bool puedeAparecerTornillo = true;

    public Transform leftHandStart;
    public Transform rightHandStart;
    public Transform leftHandEnd;
    public Transform rightHandEnd;
    private Vector3 leftHandInitialPos;
    private Vector3 rightHandInitialPos;

    public GameObject monster;
    private bool isAttacking = false;
    public bool puedeAtacar = true;

    [Header("Camera Look")]
    public float sensibilidad = 120f;
    public float limiteArriba = 60f;
    public float limiteAbajo = -60f;

    float rotacionVertical = 0f;

    public GameObject TeclaW;
    public GameObject TeclaS;
    public GameObject TeclaF;
    public GameObject TeclaA;
    public GameObject TeclaD;
    public GameObject TeclaR;

    public TMP_Text temporizadorTexto;

    [Header("QTE")]
    float tiempoPorTecla = 2.5f;
    int teclasNecesarias = 4;

    private bool QTEActivo = false;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    public Vector3 rotacionMuerteEuler = new Vector3(0f, 180f, 0f);
    public Vector3 rotacionMuerte2Euler = new Vector3(-40f, 180f, 0f);
    public Vector3 rotacionMuerte3Euler = new Vector3(-70f, 180f, 0f);


    private bool puedeGirarCamara = true;

    Coroutine lucesCoroutine;

    public Transform startPoseBrazoMuerte;
    public Transform endPoseBrazoMuerte;
    public GameObject brazoMonstruo;

    public GameObject monstruoFinal;
    public GameObject bocaMonster;
    public Transform monsterFinalPos;
    public Transform monsterFinalPosSusto;

    public GameObject pantallaNegro;

    public TMP_Text TemporizadorNivel1;
    public TMP_Text TemporizadorNivel2;
    public TMP_Text TemporizadorNivel3;

    [Header("Temporizador Nivel")]
    float tiempoNivel = 180f;
    private bool acaboElTiempo = false;

    public GameObject LuzMonster;

    private bool yaTocoTornillo = false;
    public bool estaMuriendo = false;
    Coroutine monsterCoroutine;
    Coroutine tornilloCoroutine;
    bool esperandoSiguienteTecla = false;



    KeyCode GetRandomKey()
    {
        KeyCode[] teclas = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.R };
        return teclas[Random.Range(0, teclas.Length)];
    }
    void MostrarTecla(KeyCode key)
    {
        TeclaW.SetActive(key == KeyCode.W);
        TeclaA.SetActive(key == KeyCode.A);
        TeclaS.SetActive(key == KeyCode.S);
        TeclaD.SetActive(key == KeyCode.D);
        TeclaF.SetActive(key == KeyCode.F);
        TeclaR.SetActive(key == KeyCode.R);
    }

    void OcultarTeclas()
    {
        TeclaW.SetActive(false);
        TeclaA.SetActive(false);
        TeclaS.SetActive(false);
        TeclaD.SetActive(false);
        TeclaF.SetActive(false);
        TeclaR.SetActive(false);
    }




    void Start()
    {
        StartCoroutine(FadeOutPantallaNegra(2f));
        monsterCoroutine = StartCoroutine(Monster());
        lucesCoroutine = StartCoroutine(LucesParpadeo());

        leftHandInitialPos = leftHandStart.position;
        rightHandInitialPos = rightHandStart.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pantallaNegro.SetActive(false);
    }

    private IEnumerator FadeOutPantallaNegra(float duration)
    {
        pantallaNegra.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CanvasGroup canvasGroup = pantallaNegra.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = pantallaNegra.AddComponent<CanvasGroup>();
        }
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        pantallaNegra.SetActive(false);
    }




    private void Update()
    {
        //float tiempoNivel = Time.timeSinceLevelLoad;

        if(GameManager.Instance.CompletoTodosLosMinijuegos == true)
        {
            tiempoNivel = 170f;
            tiempoPorTecla = 1.5f;
            teclasNecesarias = 6;
            Light1.GetComponent<Light>().color = Color.red;
            Light2.GetComponent<Light>().color = Color.red;
            Light3.GetComponent<Light>().color = Color.red;
            GameManager.Instance.Guardar();
        }

        if(haTocadoTornillo == true && yaTocoTornillo == false)
        {
            StartCoroutine(Muerte());
            yaTocoTornillo = true;
        }

        if (!acaboElTiempo)
        {
            tiempoNivel -= Time.deltaTime;
            tiempoNivel = Mathf.Max(tiempoNivel, 0f);

            string tiempoTexto = Mathf.Ceil(tiempoNivel).ToString();
            TemporizadorNivel1.text = tiempoTexto;
            TemporizadorNivel2.text = tiempoTexto;
            TemporizadorNivel3.text = tiempoTexto;

            if (tiempoNivel <= 0f)
            {
                acaboElTiempo = true;
                StartCoroutine(Muerte());
            }
        }

        if (Input.GetKey(KeyCode.W) && isAttacking == false)
        {
            Vector3 direccion = player.transform.forward;
            direccion.y = 0f;
            direccion.Normalize();

            player.transform.position += direccion * 1.2f * Time.deltaTime;
        }

        if (puedeGirarCamara == true)
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

            rotacionVertical -= mouseY;
            rotacionVertical = Mathf.Clamp(rotacionVertical, limiteAbajo, limiteArriba);

            cameraTransform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);
        }

        if (puedeAparecerTornillo)
        {
            tornilloCoroutine = StartCoroutine(AparecerTornillo());
            puedeAparecerTornillo = false;
        }


    }


    public IEnumerator Monster()
    {
        while (puedeAtacar)
        {
            yield return new WaitForSeconds(6f);

            if (estaMuriendo) yield break;
            if (Random.Range(0, 2) != 0) continue;

            isAttacking = true;
            LuzMonster.SetActive(true);

            float tiempo = 0f;
            float duracion = 0.4f;

            Vector3 leftStart = leftHandStart.position;
            Vector3 rightStart = rightHandStart.position;

            while (tiempo < duracion)
            {
                if (estaMuriendo) yield break;

                tiempo += Time.deltaTime;
                float t = tiempo / duracion;

                leftHandStart.position = Vector3.Lerp(leftStart, leftHandEnd.position, t);
                rightHandStart.position = Vector3.Lerp(rightStart, rightHandEnd.position, t);
                yield return null;
            }

            yield return StartCoroutine(QTESystem());
        }
    }



    IEnumerator RetirarManos()
    {
        LuzMonster.SetActive(false);
        float tiempo = 0f;
        float duracion = 0.4f;

        Vector3 leftFrom = leftHandStart.position;
        Vector3 rightFrom = rightHandStart.position;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            leftHandStart.position = Vector3.Lerp(leftFrom, leftHandInitialPos, t);
            rightHandStart.position = Vector3.Lerp(rightFrom, rightHandInitialPos, t);
            yield return null;
        }

        leftHandStart.position = leftHandInitialPos;
        rightHandStart.position = rightHandInitialPos;

        isAttacking = false;
    }



    IEnumerator QTESystem()
    {
        QTEActivo = true;
        bool fallo = false;

        for (int i = 0; i < teclasNecesarias; i++)
        {
            if (estaMuriendo) yield break;

            KeyCode teclaActual = GetRandomKey();
            MostrarTecla(teclaActual);

            float tiempoRestante = tiempoPorTecla;
            bool acertada = false;

            while (tiempoRestante > 0f)
            {
                if (estaMuriendo)
                {
                    OcultarTeclas();
                    temporizadorTexto.text = "";
                    QTEActivo = false;
                    yield break;
                }

                temporizadorTexto.text = tiempoRestante.ToString("F2");
                temporizadorTexto.color = tiempoRestante < 1f ? Color.red : Color.white;

                if (!esperandoSiguienteTecla && Input.GetKeyDown(teclaActual))
                {
                    acertada = true;
                    esperandoSiguienteTecla = true;
                    break;
                }

                if (!esperandoSiguienteTecla && Input.anyKeyDown)
                {
                    fallo = true;
                    break;
                }

                tiempoRestante -= Time.deltaTime;
                yield return null;
            }

            OcultarTeclas();
            temporizadorTexto.text = "";
            yield return null;

            esperandoSiguienteTecla = false;

            if (!acertada)
            {
                fallo = true;
                break;
            }

            yield return new WaitForSeconds(0.4f);
        }

        QTEActivo = false;

        if (!fallo && !estaMuriendo)
            yield return StartCoroutine(RetirarManos());

        if (fallo && !estaMuriendo)
            StartCoroutine(Muerte());
    }



    IEnumerator LucesParpadeo()
    {

        while (true)
        {
            Light1.SetActive(false);
            Light2.SetActive(false);
            Light3.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            int ramdomLuz = Random.Range(0, 3);
            if (ramdomLuz == 1)
            {
                Light1.SetActive(false);
                Light2.SetActive(true);
                Light3.SetActive(false);
            }
            else if (ramdomLuz == 2)
            {
                Light1.SetActive(false);
                Light2.SetActive(false);
                Light3.SetActive(true);
            }
            else
            {
                Light1.SetActive(true);
                Light2.SetActive(false);
                Light3.SetActive(false);
            }
            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator Muerte()
    {
        if (estaMuriendo) yield break;
        estaMuriendo = true;

        ForzarRetiradaManos();

        if (monsterCoroutine != null)
            StopCoroutine(monsterCoroutine);

        if (lucesCoroutine != null)
            StopCoroutine(lucesCoroutine);

        if (tornilloCoroutine != null)
            StopCoroutine(tornilloCoroutine);

        LuzMonster.SetActive(false);
        isAttacking = true;
        puedeAtacar = false;
        puedeAparecerTornillo = false;
        puedeGirarCamara = false;

        if (lucesCoroutine != null)
            StopCoroutine(lucesCoroutine);

        Light1.SetActive(true);
        Light2.SetActive(true);
        Light3.SetActive(true);

        // Fase 1: giro horizontal
        yield return StartCoroutine(GirarCamara(
            cameraPlayer.transform.localRotation,
            Quaternion.Euler(rotacionMuerteEuler),
            0.8f
        ));
        yield return new WaitForSeconds(0.4f);
        // Fase 2: caída
        yield return StartCoroutine(GirarCamara(
            cameraPlayer.transform.localRotation,
            Quaternion.Euler(rotacionMuerte2Euler),
            1f
        ));
        yield return new WaitForSeconds(0.4f);
        float tiempo = 0f;
        float duracion = 0.8f;
        Quaternion desde = brazoMonstruo.transform.localRotation;
        Quaternion hasta = Quaternion.Euler(rotacionMuerte3Euler);

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            brazoMonstruo.transform.localRotation = Quaternion.Slerp(desde, hasta, t);
            yield return null;
        }

        brazoMonstruo.transform.localRotation = hasta;

        pantallaNegro.SetActive(true);
        monstruoFinal.transform.position = monsterFinalPos.transform.position;
        yield return new WaitForSeconds(0.2f);
        pantallaNegro.SetActive(false);

        float duracionApertura = 1f;
        tiempo = 0f;
        Vector3 bocaInicialScale = bocaMonster.transform.localScale;
        Vector3 bocaFinalScale = new Vector3(bocaInicialScale.x, bocaInicialScale.y * 2f, bocaInicialScale.z);
        while (tiempo < duracionApertura)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionApertura;
            bocaMonster.transform.localScale = Vector3.Lerp(bocaInicialScale, bocaFinalScale, t);
            yield return null;
        }
        bocaMonster.transform.localScale = bocaFinalScale;
        

        float duracionFinal = 0.1f;
        tiempo = 0f;
        Vector3 posicionInicial = monstruoFinal.transform.position;
        Vector3 posicionFinal = monsterFinalPosSusto.transform.position;
        while (tiempo < duracionFinal)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionFinal;
            monstruoFinal.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, t);
            yield return null;
        }

        pantallaNegro.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");

    }

    IEnumerator GirarCamara(Quaternion desde, Quaternion hasta, float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            cameraPlayer.transform.localRotation = Quaternion.Slerp(desde, hasta, t);
            yield return null;
        }

        cameraPlayer.transform.localRotation = hasta;
    }

    void ForzarRetiradaManos()
    {
        LuzMonster.SetActive(false);

        leftHandStart.position = leftHandInitialPos;
        rightHandStart.position = rightHandInitialPos;

        isAttacking = false;
    }


    IEnumerator AparecerTornillo()
    {
        while (!hayTornillo && !estaMuriendo)
        {
            yield return new WaitForSeconds(2f);

            if (Random.value < 0.5f && !haTocadoTornillo)
            {
                Instantiate(Tornillo, puntoTornillo.position, puntoTornillo.rotation);
                hayTornillo = true;
            }
        }
    }

}
