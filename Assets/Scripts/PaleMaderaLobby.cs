using StarterAssets;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class PaleMaderaLobby : ObjetoInteractivo
{
    ThirdPersonController controller;
    public GameObject Player;
    public Transform PuntoIdaPlayer;
    public float TiempoEnIr = 1.5f;

    public CapsuleCollider ColliderPlayer;
    public Rigidbody PaleRigidBody;
    public bool QuitarCollider = true;

    bool interactuado = false;
    FieldInfo yawField;

    public AudioSource pale;

    public void Start()
    {
        PaleRigidBody.isKinematic = true;
        PaleRigidBody.useGravity = false;

        controller = Player.GetComponent<ThirdPersonController>();

        yawField = typeof(ThirdPersonController).GetField(
            "_cinemachineTargetYaw",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
    }

    public override void ActivarObjeto()
    {
        if (interactuado) return;
        StartCoroutine(TirarPale());
    }

    public void Interactuar()
    {
        ColliderPlayer.enabled = false;
        controller.enabled = false;
    }

    public void ReactivarJugador()
    {
        ColliderPlayer.enabled = true;
        controller.enabled = true;
        SincronizarCamara();
    }

    public void SincronizarCamara()
    {
        if (yawField != null)
            yawField.SetValue(controller, Player.transform.eulerAngles.y);
    }

    public IEnumerator TirarPale()
    {
        interactuado = true;
        Interactuar();

        Vector3 posInicial = Player.transform.position;
        Quaternion rotInicial = Player.transform.rotation;

        float t = 0f;

        // FASE 1 – Movimiento + rotación (la cámara sigue porque Cinemachine está activo)
        while (t < TiempoEnIr)
        {
            t += Time.deltaTime;
            float k = t / TiempoEnIr;

            Player.transform.position = Vector3.Lerp(posInicial, PuntoIdaPlayer.position, k);
            Player.transform.rotation = Quaternion.Slerp(rotInicial, PuntoIdaPlayer.rotation, k);

            yield return null;
        }

        Player.transform.position = PuntoIdaPlayer.position;
        Player.transform.rotation = PuntoIdaPlayer.rotation;
        SincronizarCamara();

        yield return new WaitForSeconds(0.3f);

        // FASE 2 – Tirar el palé
        PaleRigidBody.isKinematic = false;
        PaleRigidBody.useGravity = true;
        PaleRigidBody.AddForce(Vector3.back * 3f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f);
        pale.Play();

        yield return new WaitForSeconds(0.8f);

        // FASE 3 – Devolver control al jugador
        QuitarCollider = false;
        ReactivarJugador();

        yield return new WaitForSeconds(2f);
        GameManager.Instance.yaCayoPaleMadera = true;
        GameManager.Instance.Guardar();
    }

    void Update()
    {
        if(GameManager.Instance.yaCayoPaleMadera == true)
        {
            Destroy(this.gameObject);
        }
    }
}
