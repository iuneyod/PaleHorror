using UnityEngine;

public class VideoCameraFollowPlayer : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform player;

    [Header("Opciones de seguimiento")]
    public float rotationSpeed = 6f; // qué tan rápido gira hacia el jugador

    public Vector3 rotationOffsetEuler = new Vector3(0, 90, 0); // ajustar según tu modelo
    private Quaternion rotationOffset;

    void Start()
    {
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }


    void Update()
    {
        if (player == null) return;

        // Dirección hacia el jugador
        Vector3 direccion = player.position - transform.position;


        if (direccion.sqrMagnitude > 0.001f) // evitar errores si está muy cerca
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion) * rotationOffset;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, rotationSpeed * Time.deltaTime);

        }
    }
}
