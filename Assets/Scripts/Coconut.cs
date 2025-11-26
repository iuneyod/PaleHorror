using StarterAssets;
using System.Collections;
using UnityEngine;

public class Coconut : MonoBehaviour
{
    public int healAmount = 10;
    public bool TakinCoconut = false;
    public bool enArea = false;
    public GameObject outlineMesh;
    private Animator animator;
    public Transform carryPoint;
    public bool isCarryingCoconut = false;

    AxeTool axeTool;
    Tomato tomato;
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
        axeTool = FindObjectOfType<AxeTool>();
        tomato = FindObjectOfType<Tomato>();
        Rock = FindObjectOfType<Rock>();
        hammer1 = FindObjectOfType<Hammer1>();

        enArea = false;
        TakinCoconut = false;
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
        if (Input.GetKeyDown(KeyCode.E) && enArea && !TakinCoconut && !CarryState.IsCarryingAnything())
        {
            TakinCoconut = true;
            if (outlineMesh != null)
                outlineMesh.SetActive(false);

            if (animator != null)
                animator.SetTrigger("PickUp");

            StartCoroutine(EatCoconut());
        }

        if (Input.GetKeyDown(KeyCode.T) && enArea && !TakinCoconut && !CarryState.IsCarryingAnything())
        {
            if (isCarryingCoconut == false)
            {
                CarryState.carryingCoconut = true;
                isCarryingCoconut = true;
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

        if (Input.GetKeyDown(KeyCode.F) && isCarryingCoconut == true)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        CarryState.carryingCoconut = false;
        transform.SetParent(null);
        isCarryingCoconut = false;
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

    private IEnumerator EatCoconut()
    {
        if (TakinCoconut)
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
            TakinCoconut = false;

            Destroy(gameObject);
        }
    }
}
