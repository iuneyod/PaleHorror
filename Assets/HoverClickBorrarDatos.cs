using System.Collections;
using UnityEngine;

public class HoverClickBorrarDatos : MonoBehaviour
{
    private bool hover;
    private bool estabaHover;

    public GameObject menuDatos;
    public GameObject papelera;

    public float alturaHover = 0.6f;
    public float duracion = 0.2f;

    private Vector3 posicionInicial;
    private Coroutine movimientoActual;

    void Start()
    {
        posicionInicial = papelera.transform.position;
    }

    void Update()
    {
        DetectarHover();

        if (hover && !estabaHover)
            MoverPapelera(posicionInicial + Vector3.up * alturaHover);

        if (!hover && estabaHover)
            MoverPapelera(posicionInicial);

        estabaHover = hover;

        DetectarClick();
    }

    void DetectarHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        hover = Physics.Raycast(ray, out hit) && hit.transform == transform;
    }

    void MoverPapelera(Vector3 destino)
    {
        if (movimientoActual != null)
            StopCoroutine(movimientoActual);

        movimientoActual = StartCoroutine(MoverSuave(destino));
    }

    IEnumerator MoverSuave(Vector3 destino)
    {
        float tiempo = 0f;
        Vector3 inicio = papelera.transform.position;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            papelera.transform.position = Vector3.Lerp(inicio, destino, t);
            yield return null;
        }

        papelera.transform.position = destino;
    }

    void DetectarClick()
    {
        if (hover && Input.GetMouseButtonDown(0))
        {
            menuDatos.SetActive(true);
        }
    }
}
