using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AparecerCasaIglesia : MonoBehaviour
{
    public GameObject CasaIglesia;
    public GameObject objetosPrimeraCasa;
    public GameObject objetosSegundaCasa;
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Efectos;
    public BoxCollider TriggerCollider;
    public GameObject mainCamera;

    public bool tieneLlaveCasaIglesia = false;
    public bool enArea = false;

    private bool edificioActivado = false;


    private void Start()
    {
        CasaIglesia.SetActive(false);
        Efectos.SetActive(false);
        objetosPrimeraCasa.SetActive(true);
        objetosSegundaCasa.SetActive(false);

        Monster1.SetActive(false);
        Monster2.SetActive(false);

    }

    private void Update()
    {
        // Recoger llave
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            tieneLlaveCasaIglesia = true;
            TriggerCollider.enabled = false;
            StartCoroutine(DesactivarLlaveDespues());
            this.gameObject.transform.position = new Vector3(0, 0, 0);
        }


        // Activar edificio una sola vez
        if (tieneLlaveCasaIglesia && !edificioActivado)
        {
            edificioActivado = true;
            StartCoroutine(AparecerEdificio());
            objetosPrimeraCasa.SetActive(false);
            objetosSegundaCasa.SetActive(false);
        }
    }
    private IEnumerator DesactivarLlaveDespues()
    {
        yield return new WaitForSeconds(25f);
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enArea = false;
    }

    private IEnumerator AparecerEdificio()
    {
        CasaIglesia.SetActive(true);
        Efectos.SetActive(true);

        yield return new WaitForSeconds(2f);
        StartCoroutine(CameraShake(4f, 0.15f)); // (duración = 0.5s, intensidad = 0.15)
        Monster1.SetActive(true);
        Monster2.SetActive(true);
        yield return new WaitForSeconds(4f);
        Monster1.SetActive(false);
        Monster2.SetActive(false);

        yield return new WaitForSeconds(3f);
        StartCoroutine(CameraShake(4f, 0.3f)); // (duración = 0.5s, intensidad = 0.15)
        Monster1.SetActive(true);
        Monster2.SetActive(true);

        yield return new WaitForSeconds(4f);
        Monster1.SetActive(false);
        Monster2.SetActive(false);

        yield return new WaitForSeconds(3f);

        Efectos.SetActive(false);
        CasaIglesia.SetActive(false);
    }

    private IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCamera.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;  // restaurar posición
    }

}
