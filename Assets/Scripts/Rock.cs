using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public static bool anyRockCarried = false; // <- nueva variable global

    public bool TakinRock = false;
    public bool enArea = false;
    public GameObject outlineMesh;
    private Animator animator;
    public Transform carryPoint;
    public bool isCarryingRock = false;

    Coconut coconut;
    Tomato tomato;
    AxeTool AxeTool;
    Hammer1 hammer1;
    Rock rock;

    public Collider myCollider;
    public Rigidbody myRb;

    private void Start()
    {
        if (outlineMesh != null)
            outlineMesh.SetActive(false);

        ThirdPersonController player = FindObjectOfType<ThirdPersonController>();
        if (player != null)
            animator = player.GetComponent<Animator>();

        myCollider = GetComponent<Collider>();
        myRb = GetComponent<Rigidbody>();
        coconut = FindObjectOfType<Coconut>();
        tomato = FindObjectOfType<Tomato>();
        AxeTool = FindObjectOfType<AxeTool>();
        hammer1 = FindObjectOfType<Hammer1>();

        enArea = false;
        TakinRock = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
            if (outlineMesh != null)
                outlineMesh.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
            if (outlineMesh != null)
                outlineMesh.SetActive(false);
        }
    }

    private void Update()
    {
        // No permitir coger otra si ya hay una roca cargada
        if (Input.GetKeyDown(KeyCode.T) && enArea && !TakinRock && !CarryState.IsCarryingAnything())

        {
            PickUp();
        }

        if (Input.GetKeyDown(KeyCode.F) && isCarryingRock)
        {
            DropObject();
        }
    }

    void PickUp()
    {
        CarryState.carryingRock = true;
        isCarryingRock = true;
        anyRockCarried = true; // <- marca que ya se lleva una
        transform.SetParent(carryPoint);
        outlineMesh.SetActive(false);
        enArea = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(-12, -200, -100);

        if (myCollider) myCollider.enabled = false;
        if (myRb)
        {
            myRb.isKinematic = true;
            myRb.useGravity = false;
        }
    }

    public void DropObject()
    {
        CarryState.carryingRock = false;
        transform.SetParent(null);
        isCarryingRock = false;
        anyRockCarried = false; // <- permite coger otra
        outlineMesh.SetActive(true);
        enArea = true;

        if (myCollider) myCollider.enabled = true;
        if (myRb)
        {
            myRb.isKinematic = false;
            myRb.useGravity = true;
        }
    }
}
