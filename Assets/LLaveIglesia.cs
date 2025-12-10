using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLaveIglesia : MonoBehaviour
{
    public bool enArea = false;
    public bool tieneLlaveIglesia = false;


    private void Update()
    {
        if (enArea && Input.GetKeyDown(KeyCode.E))
        {
            tieneLlaveIglesia = true;
            this.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }
}
