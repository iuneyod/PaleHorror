using StarterAssets;
using System.Collections;
using UnityEngine;

public class AxeTool : MonoBehaviour
{
    public bool TakinAxe = false;
    public bool enArea = false;
    public GameObject outlineMesh;
    private Animator animator;
    public Transform carryPoint;
    public bool isCarryingAxe = false;

    Coconut coconut;
    Tomato tomato;
    Comon_Mutant mutant;
    Rock Rock;
    Hammer1 hammer1;

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
        hammer1 = FindObjectOfType<Hammer1>();

        enArea = false;
        TakinAxe = false;
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

        if (Input.GetKeyDown(KeyCode.T) && enArea && !TakinAxe && !CarryState.IsCarryingAnything())
        {
            if (isCarryingAxe == false)
            {
                CarryState.carryingAxe = true;
                isCarryingAxe = true;
                transform.SetParent(carryPoint);
                outlineMesh.SetActive(false);
                enArea = false;
                mutant.HasAxe = true;
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

        if (Input.GetKeyDown(KeyCode.F) && isCarryingAxe == true)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        CarryState.carryingAxe = false;
        transform.SetParent(null);
        isCarryingAxe = false;
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
