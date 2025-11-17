using StarterAssets;
using System.Collections;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public int healAmount = 5;
    public bool TakinTomato = false;
    public bool enArea = false;
    public GameObject outlineMesh;
    private Animator animator;
    public Transform carryPoint;
    public bool isCarryingTomato = false;

    AxeTool axeTool;
    Coconut coconut;
    Rock rock;
    Hammer1 Hammer1;

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
        axeTool = FindObjectOfType<AxeTool>();
        coconut = FindObjectOfType<Coconut>();
        rock = FindObjectOfType<Rock>();
        Hammer1 = FindObjectOfType<Hammer1>();

        enArea = false;
        TakinTomato = false;
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
        if (Input.GetKeyDown(KeyCode.E) && enArea && !TakinTomato && !CarryState.IsCarryingAnything())
        {
            TakinTomato = true;
            if (outlineMesh != null)
                outlineMesh.SetActive(false);

            if (animator != null)
                animator.SetTrigger("PickUp");

            StartCoroutine(EatTomato());
        }

        if (Input.GetKeyDown(KeyCode.T) && enArea && !TakinTomato && !CarryState.IsCarryingAnything())
        {
            if (isCarryingTomato == false)
            {
                CarryState.carryingTomato = true;
                isCarryingTomato = true;
                transform.SetParent(carryPoint);
                outlineMesh.SetActive(false);
                enArea = false;
                Player_Stats playerStats = FindObjectOfType<Player_Stats>();
                playerStats.canPunch = false;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                if (myCollider) myCollider.enabled = false;
                if (myRb)
                {
                    myRb.isKinematic = true;
                    myRb.useGravity = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && isCarryingTomato == true)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        CarryState.carryingTomato = false;
        transform.SetParent(null);
        isCarryingTomato = false;
        outlineMesh.SetActive(true);
        enArea = true;
        Player_Stats playerStats = FindObjectOfType<Player_Stats>();
        playerStats.canPunch = true;

        if (myCollider) myCollider.enabled = true;
        if (myRb)
        {
            myRb.isKinematic = false;
            myRb.useGravity = true;
        }
    }

    private IEnumerator EatTomato()
    {
        if (TakinTomato)
        {
            ThirdPersonController player = FindObjectOfType<ThirdPersonController>();
            StarterAssetsInputs playerInput = FindObjectOfType<StarterAssetsInputs>();
            playerInput.enabled = false;
            player.enabled = false;
            player.MoveSpeed = 0;
            player.SprintSpeed = 0;
            player.JumpHeight = 0;

            yield return new WaitForSeconds(3.45f);
            player.enabled = true;
            yield return new WaitForSeconds(0.05f);

            if (animator != null)
                animator.ResetTrigger("PickUp");

            Player_Stats playerStats = FindObjectOfType<Player_Stats>();
            if (playerStats != null)
                playerStats.Curar(healAmount);
            playerInput.enabled = true;
            player.MoveSpeed = 2.0f;
            player.SprintSpeed = 5.335f;
            player.JumpHeight = 1.2f;
            TakinTomato = false;

            Destroy(gameObject);
        }
    }
}
