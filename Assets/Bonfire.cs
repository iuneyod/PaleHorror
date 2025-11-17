using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public bool enArea = false;
    public GameObject outlineMesh;
    public GameObject rocksMesh;
    public Transform placePoint1;
    public Transform placePoint2;
    public Transform placePoint3;
    public Transform placePoint4;
    public Transform placePoint5;
    public Transform placePoint6;
    public Transform placePoint7;
    public Transform placePoint8;
    public Transform placePoint9;
    public Transform placePoint10;
    public Transform placePoint11;
    public Transform placePoint12;

    public bool placed1 = false;
    public bool placed2 = false;
    public bool placed3 = false;
    public bool placed4 = false;
    public bool placed5 = false;
    public bool placed6 = false;
    public bool placed7 = false;
    public bool placed8 = false;
    public bool placed9 = false;
    public bool placed10 = false;
    public bool placed11 = false;
    public bool placed12 = false;

    public bool isPlacingRock = false;

    public bool BonfireFinished1 = false;
    public GameObject fire;

    Rock rock;

    private void Start()
    {
        if (outlineMesh != null)
            outlineMesh.SetActive(false);
        enArea = false;
        isPlacingRock = false;

        rock = FindObjectOfType<Rock>();
        fire.SetActive(false);
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enArea)
        {
            ColocarPiedra();
        }

        if (placed1 && placed2 && placed3 && placed4 && placed5 && placed6 &&
            placed7 && placed8 && placed9 && placed10 && placed11 && placed12)
        {
            BonfireFinished1 = true;
        }

        if (BonfireFinished1)
        {
            fire.SetActive(true);
            rocksMesh.SetActive(false);
            outlineMesh.SetActive(false);
        }
    }

    void ColocarPiedra()
    {
        CarryState.carryingRock = false;
        Rock[] allRocks = FindObjectsOfType<Rock>();
        Rock carriedRock = null;

        foreach (Rock r in allRocks)
        {
            if (r.isCarryingRock)
            {
                carriedRock = r;
                break;
            }
        }

        if (carriedRock == null) return; // No llevas ninguna piedra

        Transform targetPoint = null;

        if (!placed1) { placed1 = true; targetPoint = placePoint1; }
        else if (!placed2) { placed2 = true; targetPoint = placePoint2; }
        else if (!placed3) { placed3 = true; targetPoint = placePoint3; }
        else if (!placed4) { placed4 = true; targetPoint = placePoint4; }
        else if (!placed5) { placed5 = true; targetPoint = placePoint5; }
        else if (!placed6) { placed6 = true; targetPoint = placePoint6; }
        else if (!placed7) { placed7 = true; targetPoint = placePoint7; }
        else if (!placed8) { placed8 = true; targetPoint = placePoint8; }
        else if (!placed9) { placed9 = true; targetPoint = placePoint9; }
        else if (!placed10) { placed10 = true; targetPoint = placePoint10; }
        else if (!placed11) { placed11 = true; targetPoint = placePoint11; }
        else if (!placed12) { placed12 = true; targetPoint = placePoint12; }

        if (targetPoint == null) return; // ya están todas puestas

        carriedRock.outlineMesh.SetActive(false);
        carriedRock.transform.SetParent(null);
        carriedRock.isCarryingRock = false;
        Rock.anyRockCarried = false; // <- libera el estado global
        carriedRock.TakinRock = false;
        carriedRock.transform.position = targetPoint.position;
        carriedRock.transform.rotation = targetPoint.rotation;
        carriedRock.transform.localScale = targetPoint.localScale;

        // Desactivar colisión y evitar volver a recogerla
        if (carriedRock.myCollider) carriedRock.myCollider.enabled = false;
        if (carriedRock.myRb)
        {
            carriedRock.myRb.isKinematic = true;
            carriedRock.myRb.useGravity = false;
        }

        // Marcarla como colocada para no volver a recogerla
        carriedRock.enArea = false;
        carriedRock.enabled = false;

        isPlacingRock = true;
    }


}
