using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowToy : ObjetoInteractivo
{
    public GameObject Crow;
    public Rigidbody rigidbodyCrow;
    public GameObject ColliderBoxes;
    public GameObject ColliderBoxes2;
    public GameObject ColliderBoxes3;
    public bool activadoCrow = false;
    public AudioSource crow;


    public override void ActivarObjeto()
    {
        if (activadoCrow) return;
        ActivarCuervo();
    }

    private void Start()
    {
        // Al inicio el reloj NO usa físicas
        rigidbodyCrow.isKinematic = true;
        rigidbodyCrow.useGravity = false;
    }

    public void ActivarCuervo()
    {
        if (activadoCrow) return;
        activadoCrow = true;
        ColliderBoxes.SetActive(false);
        ColliderBoxes2.SetActive(false);
        ColliderBoxes3.SetActive(false);

        rigidbodyCrow.isKinematic = false;
        rigidbodyCrow.useGravity = true;
        rigidbodyCrow.WakeUp();
        rigidbodyCrow.AddForce(Vector3.back * 150f, ForceMode.Impulse);
        crow.Play();



    }
}
