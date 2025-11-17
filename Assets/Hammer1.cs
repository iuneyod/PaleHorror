using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer1 : MonoBehaviour
{
    public bool TakinHammer = false;
    public bool enArea = false;
    public GameObject outlineMesh;
    private Animator animator;
    public Transform carryPoint;
    public bool isCarryingHammer = false;

    Coconut coconut;
    Tomato tomato;
    Comon_Mutant mutant;
    Rock Rock;
    AxeTool AxeTool;

    private Collider myCollider;
    private Rigidbody myRb;

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
        mutant = FindObjectOfType<Comon_Mutant>();
        Rock = FindObjectOfType<Rock>();
        AxeTool = FindObjectOfType<AxeTool>();

        enArea = false;
        TakinHammer = false;
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

        if (Input.GetKeyDown(KeyCode.T) && enArea && !TakinHammer! && !CarryState.IsCarryingAnything())
        {
            if (isCarryingHammer == false)
            {
                CarryState.carryingHammer = true;
                isCarryingHammer = true;
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
        }

        if (Input.GetKeyDown(KeyCode.F) && isCarryingHammer == true)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        CarryState.carryingHammer = false;
        transform.SetParent(null);
        isCarryingHammer = false;
        outlineMesh.SetActive(true);
        enArea = true;
        mutant.HasAxe = false;

        if (myCollider) myCollider.enabled = true;
        if (myRb)
        {
            myRb.isKinematic = false;
            myRb.useGravity = true;
        }
    }
}
