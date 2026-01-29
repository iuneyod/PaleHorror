using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockLobby : ObjetoInteractivo
{
    private int vecesInteractuado = 0;
    private int interaccionesParaCaer = 5;

    public Rigidbody rigidbodyClock;
    public GameObject Clock;
    public bool caido = false;

    private void Start()
    {
        // Al inicio el reloj NO usa físicas
        rigidbodyClock.isKinematic = true;
        rigidbodyClock.useGravity = false;
    }

    public override void ActivarObjeto()
    {
        if (caido) return;

        vecesInteractuado++;
        StartCoroutine(ClockShake(0.3f, 0.004f));

        if (vecesInteractuado >= interaccionesParaCaer)
        {
            InteractuarConReloj();
        }
    }

    public void InteractuarConReloj()
    {
        if (caido) return;

        caido = true;

        // Activamos las físicas
        rigidbodyClock.isKinematic = false;
        rigidbodyClock.useGravity = true;

        // Opcional: pequeño empujón para que se note la caída
        rigidbodyClock.AddForce(Vector3.forward * 3f, ForceMode.Impulse);
        rigidbodyClock.AddForce(Vector3.right * 3f, ForceMode.Impulse);

    }

    private IEnumerator ClockShake(float duration, float magnitude)
    {
        Vector3 originalPos = Clock.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Clock.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Clock.transform.localPosition = originalPos;  // restaurar posición
    }
}
