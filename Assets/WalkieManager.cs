using System.Collections;
using UnityEngine;

public class WalkieManager : MonoBehaviour
{
    public bool tieneWalkie = false;
    public bool walkieEnMano = false;
    public Transform carryPoint;
    public GameObject WalkiePrefab;  // PREFAB ORIGINAL

    Walkie walkie;
    GameObject walkieInstance;       //INSTANCIA EN ESCENA

    void Start()
    {
        walkie = FindObjectOfType<Walkie>();
    }

    void Update()
    {
        if (walkie.tieneWalkie)
            tieneWalkie = true;

        // SACAR WALKIE
        if (tieneWalkie && Input.GetKeyDown(KeyCode.T) && !walkieEnMano)
        {
            //CREAR INSTANCIA SI NO EXISTE
            if (walkieInstance == null)
            {
                walkieInstance = Instantiate(WalkiePrefab);
            }

            walkieInstance.SetActive(true);
            walkieInstance.transform.SetParent(carryPoint);
            walkieInstance.transform.localPosition = Vector3.zero;
            walkieInstance.transform.localRotation = Quaternion.identity;

            walkieEnMano = true;
        }

        // USAR WALKIE
        else if (walkieEnMano && Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(PrimerDialogo());
        }
    }

    IEnumerator PrimerDialogo()
    {
        // diálogos
        yield return new WaitForSeconds(0.5f);

        //GUARDAR WALKIE
        walkieInstance.transform.SetParent(null);
        walkieInstance.SetActive(false);
        walkieEnMano = false;
    }
}
