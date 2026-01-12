using UnityEngine;

public class QuitarCollider : MonoBehaviour
{
    public Collider collider1;
    public Collider collider2;
    public Collider collider3;

    public PaleMaderaLobby paleMaderaLobby;   // arrastrar aquí en el inspector

    void Update()
    {
        if (paleMaderaLobby == null) return;

        bool activo = paleMaderaLobby.QuitarCollider;

        collider1.enabled = activo;
        collider2.enabled = activo;
        collider3.enabled = activo;
    }
}
