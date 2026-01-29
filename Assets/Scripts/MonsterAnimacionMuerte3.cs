using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimacionMuerte3 : MonoBehaviour
{
    public Animator animator;
    MonsterMinigame3 monster;
    public bool matando  = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        monster = FindObjectOfType<MonsterMinigame3>();
    }

    private void Update()
    {
        if (monster.vaMatar == true && matando == false)
        {
            matando = true;
            animator.SetTrigger("AttackSpike");
        }
    }
}
