using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverFloatAndClick : MonoBehaviour
{
    [Header("Hover")]
    public float alturaHover = 0.3f;
    public float velocidadMovimiento = 5f;
    public float velocidadFlotacion = 2f;
    public float amplitudFlotacion = 0.05f;

    private Vector3 posicionInicial;
    private bool hover = false;
    private float tiempoFlotacion;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        DetectarHover();
        MoverObjeto();
        DetectarClick();
    }

    void DetectarHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        hover = Physics.Raycast(ray, out hit) && hit.transform == transform;
    }

    void MoverObjeto()
    {
        Vector3 objetivo = posicionInicial;

        if (hover)
        {
            tiempoFlotacion += Time.deltaTime * velocidadFlotacion;
            float flotacion = Mathf.Sin(tiempoFlotacion) * amplitudFlotacion;
            objetivo.y += alturaHover + flotacion;
        }
        else
        {
            tiempoFlotacion = 0f;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            objetivo,
            Time.deltaTime * velocidadMovimiento
        );
    }

    void DetectarClick()
    {
        if (hover && Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }
}
