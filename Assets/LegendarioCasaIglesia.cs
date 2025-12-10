using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendarioCasaIglesia : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetTrigger("Rage");
    }
}
