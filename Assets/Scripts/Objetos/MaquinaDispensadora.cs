using UnityEngine;
using System.Collections;

public class MaquinaDispensadora : ObjetoInteractivo
{
    bool activated = false;
    public GameObject objetoPrefab;
    public Transform spawnPoint;

    public override void ActivarObjeto()
    {
        if (activated) return;
        StartCoroutine(DarObjeto());
    }

    IEnumerator DarObjeto()
    {
        activated = true;
        yield return StartCoroutine(MaquinaShake(2f, 0.0045f));
        GameObject objeto = Instantiate(objetoPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = objeto.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.WakeUp();

            // Esperar 1 FixedUpdate antes de aplicar fuerza
            StartCoroutine(AplicarFuerzaSiguienteFrame(rb));
        }

        yield return new WaitForSeconds(2);
        activated = false;
    }

    IEnumerator AplicarFuerzaSiguienteFrame(Rigidbody rb)
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce(spawnPoint.forward * 2f, ForceMode.Impulse);
    }


    IEnumerator MaquinaShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
