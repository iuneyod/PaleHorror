using System.Collections;
using UnityEngine;

public class WalkieManager : MonoBehaviour
{
    public bool tieneWalkie = false;
    public bool walkieEnMano = false;
    public Transform carryPoint;
    public GameObject WalkiePrefab;  // PREFAB ORIGINAL

    private bool PrimerDialogoHecho = false;
    private bool SegundoDialogoHecho = false;
    private bool TercerDialogoHecho = false;
    private bool CuartoDialogoHecho = false;
    private bool QuintoDialogoHecho = false;

    Walkie walkie;
    GameObject walkieInstance;       //INSTANCIA EN ESCENA

    Campamento_Script campamento;
    VillageActivate villageActivate;
    CasaSolitariaActivate casaSolitariaActivate;
    IglesiaActivate iglesiaActivate;
    AparecerCasaIglesia aparecerCasaIglesia;

    void Start()
    {
        walkie = FindObjectOfType<Walkie>();
        campamento = FindObjectOfType<Campamento_Script>();
        villageActivate = FindObjectOfType<VillageActivate>();
        casaSolitariaActivate = FindObjectOfType<CasaSolitariaActivate>();
        iglesiaActivate = FindObjectOfType<IglesiaActivate>();
        aparecerCasaIglesia = FindObjectOfType<AparecerCasaIglesia>();
    }

    void Update()
    {
        if (walkie.tieneWalkie)
            tieneWalkie = true;

        if(campamento.Campamento_Desbloqueado == true && tieneWalkie && !walkieEnMano && PrimerDialogoHecho == false)
        {
            StartCoroutine(PrimerDialogo());
            PrimerDialogoHecho = true;
        }

        if (villageActivate.enVillage == true && tieneWalkie && !walkieEnMano && SegundoDialogoHecho == false)
        {
            StartCoroutine(SegundoDialogo());
            SegundoDialogoHecho = true;
        }

        if (casaSolitariaActivate.enCasaSolitaria == true && tieneWalkie && !walkieEnMano && TercerDialogoHecho == false)
        {
            StartCoroutine(TercerDialogo());
            TercerDialogoHecho = true;
        }

        if (iglesiaActivate.enIglesia == true && tieneWalkie && !walkieEnMano && CuartoDialogoHecho == false)
        {
            StartCoroutine(CuartoDialogo());
            CuartoDialogoHecho = true;
        }

        if(aparecerCasaIglesia.tieneLlaveCasaIglesia == true && tieneWalkie && !walkieEnMano && QuintoDialogoHecho == false)
        {
            StartCoroutine(QuintoDialogo());
            QuintoDialogoHecho = true;
        }

    }

    IEnumerator PrimerDialogo()
    {
        walkieEnMano = true;

        if (walkieInstance == null)
        {
            walkieInstance = Instantiate(WalkiePrefab);
        }

        walkieInstance.SetActive(true);
        walkieInstance.transform.SetParent(carryPoint);
        walkieInstance.transform.localPosition = Vector3.zero;
        walkieInstance.transform.localRotation = Quaternion.identity;


        yield return new WaitForSeconds(3f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);

        walkieEnMano = false;
    }

    IEnumerator SegundoDialogo()
    {
        walkieEnMano = true;

        if (walkieInstance == null)
        {
            walkieInstance = Instantiate(WalkiePrefab);
        }

        walkieInstance.SetActive(true);
        walkieInstance.transform.SetParent(carryPoint);
        walkieInstance.transform.localPosition = Vector3.zero;
        walkieInstance.transform.localRotation = Quaternion.identity;


        yield return new WaitForSeconds(3f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);

        walkieEnMano = false;
    }

    IEnumerator TercerDialogo()
    {
        walkieEnMano = true;

        if (walkieInstance == null)
        {
            walkieInstance = Instantiate(WalkiePrefab);
        }

        walkieInstance.SetActive(true);
        walkieInstance.transform.SetParent(carryPoint);
        walkieInstance.transform.localPosition = Vector3.zero;
        walkieInstance.transform.localRotation = Quaternion.identity;


        yield return new WaitForSeconds(3f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);

        walkieEnMano = false;
    }

    IEnumerator CuartoDialogo()
    {
        walkieEnMano = true;

        if (walkieInstance == null)
        {
            walkieInstance = Instantiate(WalkiePrefab);
        }

        walkieInstance.SetActive(true);
        walkieInstance.transform.SetParent(carryPoint);
        walkieInstance.transform.localPosition = Vector3.zero;
        walkieInstance.transform.localRotation = Quaternion.identity;


        yield return new WaitForSeconds(3f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);

        walkieEnMano = false;
    }

    IEnumerator QuintoDialogo()
    {
        walkieEnMano = true;

        if (walkieInstance == null)
        {
            walkieInstance = Instantiate(WalkiePrefab);
        }

        walkieInstance.SetActive(true);
        walkieInstance.transform.SetParent(carryPoint);
        walkieInstance.transform.localPosition = Vector3.zero;
        walkieInstance.transform.localRotation = Quaternion.identity;


        yield return new WaitForSeconds(3f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);

        walkieEnMano = false;
    }



}
