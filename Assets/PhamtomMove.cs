using UnityEngine;

public class PhantomMove : MonoBehaviour
{
    public float altura = 0.5f;
    public float velocidad = 1.5f;

    Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = posicionInicial + Vector3.up * y;
    }
}
