using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MINIGAME3 : MonoBehaviour
{
    public GameObject pantallaNegro;
    MonsterMinigame3 monster;
    ThirdPersonController controller;
    public GameObject lintern;
    public bool linternActive;
    public bool puedeUsarLinterna = true;
    public float duracionLinterna = 300f;
    public Image barraDuracionLinterna;
    public float bateriaMax = 300f;
    public TMP_Text PorcentajeBateria;
    private bool subiendoRuido = false;

    public GameObject mainCamera;
    public GameObject cameraAnimacion;

    public GameObject pantallaNegra;

    public float ruido;
    public float ruidoMaximo = 100f;

    public int palancasActivadas = 0;
    public int totalPalancas = 8;
    public int generadoresReparados = 0;
    public int totalGeneradores = 3;
    public bool palancasCompletadas = false;
    public bool generadoresCompletados = false;

    // Prefabs de las palancas
    public GameObject palancaPrefab;
    public GameObject generadorPrefab;
    public GameObject bateriaPrefab;

    // Lista de spawnpoints
    public List<Transform> spawnPalancas;
    public List<Transform> spawnGeneradores;
    public List<Transform> spawnPilas;

    public float linternRange = 8f;
    public LayerMask enemyMask;

    public Image barraRuido;

    public bool muriendo = false;
    public GameObject piezaBarraRuido1;
    public GameObject piezaBarraRuido2;
    public GameObject piezaBarraRuido3;
    public GameObject piezaBarraRuido4;

    public GameObject puertaIZQIZQ;
    public GameObject puertaIZQDER;
    public GameObject puertaDERIZQ;
    public GameObject puertaDERDER;

    public TMP_Text totalPalancasHechas;
    public TMP_Text totalGeneradoresHechos;
    public TMP_Text textoFinal;

    public GameObject finalLight;




    void Start()
    {
        StartCoroutine(FadeOutPantallaNegra(1.5f));
        lintern.SetActive(false);
        CrearPalancas();
        CrearGeneradores();
        CrearPilas();
        monster = FindObjectOfType<MonsterMinigame3>();
        controller = FindObjectOfType<ThirdPersonController>();
        bateriaMax = duracionLinterna;
        cameraAnimacion.SetActive(false);
        pantallaNegra.SetActive(false);
        textoFinal.gameObject.SetActive(false);
        finalLight.SetActive(false);
    }

    private IEnumerator FadeOutPantallaNegra(float duration)
    {
        pantallaNegro.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CanvasGroup canvasGroup = pantallaNegro.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = pantallaNegro.AddComponent<CanvasGroup>();
        }
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        pantallaNegro.SetActive(false);
    }

    void Update()
    {
        totalPalancasHechas.text = "Palancas: " + palancasActivadas.ToString() + " / " + totalPalancas.ToString();
        totalGeneradoresHechos.text = "Generadores: " + generadoresReparados.ToString() + " / " + totalGeneradores.ToString();


        if (duracionLinterna > bateriaMax)
        {
            duracionLinterna = bateriaMax;
        }

        barraRuido.fillAmount = ruido / ruidoMaximo;

        monster.rangoVision = 12f + (ruido / ruidoMaximo) * 10f;

        PorcentajeBateria.text = Mathf.CeilToInt((duracionLinterna / bateriaMax) * 100f).ToString() + "%";

        barraDuracionLinterna.fillAmount = duracionLinterna / 300f;

        float porcentaje = duracionLinterna / bateriaMax;
        porcentaje = Mathf.Clamp01(porcentaje);

        // De blanco a gris oscuro
        Color color = Color.Lerp(Color.black, Color.white, porcentaje);
        barraDuracionLinterna.color = color;

        if (Input.GetMouseButtonDown(0) && puedeUsarLinterna == true)
        {
            ruido += 5f;
            linternActive = !linternActive;
            lintern.SetActive(linternActive);
        }

        if (linternActive == true)
        {
            monster.rangoVision += 2f;
        }

        if (ruido >= ruidoMaximo)
        {
            ruido = ruidoMaximo;
        }

        if (ruido < 0)
            ruido = 0;

        if (linternActive)
        {
            duracionLinterna -= Time.deltaTime;
            if (duracionLinterna <= 0)
            {
                linternActive = false;
                lintern.SetActive(false);
                duracionLinterna = 0;
            }
        }

        if (palancasActivadas >= totalPalancas)
        {
            palancasCompletadas = true;
        }

        if (linternActive && puedeUsarLinterna == true)
        {
            RaycastHit hit;
            Ray ray = new Ray(lintern.transform.position, lintern.transform.forward);

            Debug.DrawRay(lintern.transform.position, lintern.transform.forward * linternRange, Color.yellow);

            if (Physics.Raycast(ray, out hit, linternRange, enemyMask))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    MonsterMinigame3 monster = hit.collider.GetComponentInParent<MonsterMinigame3>();
                    if (monster != null && !monster.enRage) // proteccion
                    {
                        monster.StartCoroutine(monster.LinternaEnEnemigo());
                    }
                }

            }
        }
        if (!subiendoRuido && ruido > 0f)
        {
            ruido -= Time.deltaTime * 20f;
        }


        if (controller.isSprinting == true)
        {
            ruido += Time.deltaTime * 2f;
            subiendoRuido = true;
        }

        if (!controller.isSprinting)
        {
            subiendoRuido = false;
        }

        if(generadoresReparados >= totalGeneradores)
        {
            generadoresCompletados = true;
        }

        if (palancasCompletadas && generadoresCompletados)
        {
            Victoria();
        }


    }

    void CrearPalancas()
    {
        int palancasAInstanciar = 8;
        List<Transform> spawnsLibres = new List<Transform>(spawnPalancas);

        for (int i = 0; i < palancasAInstanciar && spawnsLibres.Count > 0; i++)
        {
            Transform spawn = spawnsLibres[Random.Range(0, spawnsLibres.Count)];
            Instantiate(palancaPrefab, spawn.position, spawn.rotation);
            spawnsLibres.Remove(spawn);
        }
    }

    void CrearGeneradores()
    {
        int generadoresAInstanciar = 2;
        List<Transform> spawnsLibres = new List<Transform>(spawnGeneradores);

        for (int i = 0; i < generadoresAInstanciar && spawnsLibres.Count > 0; i++)
        {
            Transform spawn = spawnsLibres[Random.Range(0, spawnsLibres.Count)];
            Instantiate(generadorPrefab, spawn.position, spawn.rotation);
            spawnsLibres.Remove(spawn);
        }
    }


    void CrearPilas()
    {
        List<Transform> spawnsLibres = new List<Transform>(spawnPilas);

        int pilasAInstanciar = 2;
        for (int i = 0; i < pilasAInstanciar; i++)
        {
            Transform spawn = spawnsLibres[Random.Range(0, spawnsLibres.Count)];
            Instantiate(bateriaPrefab, spawn.position, spawn.rotation);
            spawnsLibres.Remove(spawn);
        }
    }


    void Victoria()
    {
        textoFinal.gameObject.SetActive(true);
        totalGeneradoresHechos.gameObject.SetActive(false);
        totalPalancasHechas.gameObject.SetActive(false);
        finalLight.SetActive(true);
        puertaIZQIZQ.transform.rotation = Quaternion.Euler(0, -95, 0);
        puertaIZQDER.transform.rotation = Quaternion.Euler(0, 95, 0);
        puertaDERIZQ.transform.rotation = Quaternion.Euler(0, -95, 0);
        puertaDERDER.transform.rotation = Quaternion.Euler(0, 95, 0);
    }

    public IEnumerator Muerte()
    {
        muriendo = true;
        mainCamera.SetActive(false);
        cameraAnimacion.SetActive(true);
        monster.vaMatar = true;
        barraDuracionLinterna.gameObject.SetActive(false);
        barraRuido.gameObject.SetActive(false);
        totalGeneradoresHechos.gameObject.SetActive(false);
        totalPalancasHechas.gameObject.SetActive(false);
        PorcentajeBateria.gameObject.SetActive(false);
        textoFinal.gameObject.SetActive(false);
        piezaBarraRuido1.SetActive(false);
        piezaBarraRuido2.SetActive(false);
        piezaBarraRuido3.SetActive(false);
        piezaBarraRuido4.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInPantallaNegra(1.5f));
        yield return new WaitForSeconds(1f);
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
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
    }
}