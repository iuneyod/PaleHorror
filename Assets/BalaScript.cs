using System.Collections;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    bool isDestroying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroying) return;  // Evita múltiples destrucciones
        isDestroying = true;

        // Desactiva el collider para evitar más impactos
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
