using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    public LayerMask pushLayers;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    public Player_Stats playerStats; // referencia al script de vida

    public float damageCooldown = 1f;
    private float nextDamageTime = 0f;

    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<Player_Stats>();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);

        if (hit.gameObject.CompareTag("Enemy"))
        {
            // Solo aplicar daño si ya pasó el tiempo de espera
            if (Time.time >= nextDamageTime)
            {
                if (playerStats != null)
                {
                    playerStats.TakeDamage(10);
                    Debug.Log("Golpe del enemigo detectado");
                }

                // Reiniciar el temporizador
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }

    private float pushCooldown = 0.2f;
    private float nextPushTime = 0f;

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        if (Time.time < nextPushTime) return;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

        body.velocity = Vector3.zero; // evita resistencia
        body.AddForce(pushDir.normalized * strength, ForceMode.Impulse);

        nextPushTime = Time.time + pushCooldown;
    }

}
