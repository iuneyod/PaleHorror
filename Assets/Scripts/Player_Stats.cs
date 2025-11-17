using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int Vida = 100;
    public int MaxVida = 100;
    public GameObject PunchHitBox;
    public Animator animator;
    public bool isPunching = false;
    public bool canPunch = true;

    public TMP_Text vidaText;


    private void Start()
    {
        canPunch = true;
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 80, 100, 20), "Vida");
        GUI.Box(new Rect(10, 10, Vida / MaxVida * 100, 20), "");
    }
    private void Update()
    {
        if (Vida <= 0)
        {
            Vida = 0;
        }

        if (Vida > MaxVida)
        {
            Vida = MaxVida;
        }

        if (Input.GetMouseButtonDown(0) && !isPunching && canPunch)
        {
            animator.SetBool("IsPunch", true);
            StartCoroutine(Punch());
        }

        ThirdPersonController controller = GetComponent<ThirdPersonController>();
        controller.isCojeando = (Vida <= 20);

        vidaText.text = "Vida: " + Vida.ToString();
    }

    public IEnumerator Punch()
    {
        canPunch = false; // bloquea ataque

        yield return new WaitForSeconds(0.6f);
        PunchHitBox.SetActive(true);
        isPunching = true;

        yield return new WaitForSeconds(0.12f);
        animator.SetBool("IsPunch", false);
        PunchHitBox.SetActive(false);
        isPunching = false;

        yield return new WaitForSeconds(1.5f); // cooldown
        canPunch = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyPunch"))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int damage)
    {
        Vida -= damage;
        if (Vida <= 0)
        {
            Die();
        }
    }

    public void Curar(int heal)
    {
        Vida += heal;
        if (Vida > 100)
        {
            Vida = 100; // Assuming max health is 100
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        Vida = 0;
    }

    void Revivir()
    {
        Vida = MaxVida;
        animator.SetBool("Grounded", true);
    }
}
