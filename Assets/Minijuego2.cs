using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minijuego2 : MonoBehaviour
{

    public GameObject cameraPlayer;
    public GameObject Light;
    //public GameObject Monster;
    public Transform cameraTransform;

    bool mirandoFrente = false;
    bool mirandoAtras = false;



    private void Start()
    {
        cameraPlayer.transform.parent = null;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraPlayer.transform.position +=
            cameraPlayer.transform.forward * 1.5f * Time.deltaTime;
        }
    }


    void MonsterLogic()
    {

    }
}
