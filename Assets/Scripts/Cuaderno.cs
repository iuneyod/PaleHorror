using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuaderno : MonoBehaviour
{
    public GameObject CuadernoUI;
    private bool isCuadernoOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isCuadernoOpen = !isCuadernoOpen;
            CuadernoUI.SetActive(isCuadernoOpen);
        }
    }


}
