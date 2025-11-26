using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public int Vida = 50;
    public int MaxVida = 50;
    public GameObject PunchHitBox;
    public Animator animator;
    public bool isPunching = false;
    public bool canPunch = true;
    public LayerMask Water;
    public Rigidbody rb;

    public Transform SpecialAttackPosition1;

    //efectos
    public GameObject IntroAttack1;
    public GameObject attackEffect1;
    public GameObject triggerArea1;
    public bool canSpecialAttack1 = false;

    ThirdPersonController controller;
    Level_Experience levelExperience;
    IceCrystal iceCrystal;

    public TMP_Text vidaText;


    private void Start()
    {
        canPunch = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<ThirdPersonController>();
        levelExperience = FindObjectOfType<Level_Experience>();
        iceCrystal = FindObjectOfType<IceCrystal>();
        attackEffect1.SetActive(false);
        IntroAttack1.SetActive(false);
        triggerArea1.SetActive(false);
        IntroAttack1.transform.position = SpecialAttackPosition1.position;
        attackEffect1.transform.position = SpecialAttackPosition1.position;
        triggerArea1.transform.position = SpecialAttackPosition1.position;
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

        if (Input.GetKeyDown(KeyCode.P) && isPunching == false && canPunch && iceCrystal.IceCrystal_Desbloqueado == true && canSpecialAttack1 == true)
        {
            StartCoroutine(AttackEffect1());
        }

        vidaText.text = "Vida: " + Vida.ToString();

        if(levelExperience.isLevel2 == true)
        {
            MaxVida = 60;
        }
        if (levelExperience.isLevel4 == true)
        {
            MaxVida = 70;
        }
        if (levelExperience.isLevel6 == true)
        {
            MaxVida = 80;
        }
        if (levelExperience.isLevel8 == true)
        {
            MaxVida = 90;
        }
        if (levelExperience.isLevel10 == true)
        {
            MaxVida = 100;
        }
    }

    public IEnumerator AttackEffect1()
    {
        // Guardamos su parent original (el jugador)
        Transform originalParent = attackEffect1.transform.parent;

        // Lo des-parentamos para que NO se mueva con el jugador
        attackEffect1.transform.SetParent(null, true);
        triggerArea1.transform.SetParent(null, true);
        IntroAttack1.transform.SetParent(null, true);

        // Colocarlos en la posición inicial del ataque
        attackEffect1.transform.position = SpecialAttackPosition1.position;
        triggerArea1.transform.position = SpecialAttackPosition1.position;
        IntroAttack1.transform.position = SpecialAttackPosition1.position;

        canSpecialAttack1 = false;
        IntroAttack1.SetActive(true);
        yield return new WaitForSeconds(1f);
        IntroAttack1.SetActive(false);
        attackEffect1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        triggerArea1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        triggerArea1.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        attackEffect1.SetActive(false);
        // Volver a parentar al jugador
        attackEffect1.transform.SetParent(originalParent, true);
        triggerArea1.transform.SetParent(originalParent, true);
        IntroAttack1.transform.SetParent(originalParent, true);

        yield return new WaitForSeconds(20f);
        canSpecialAttack1 = true;
    }
    public IEnumerator Punch()
    {
        canPunch = false; // bloquea ataque
        PunchHitBox.SetActive(true);
        isPunching = true;

        yield return new WaitForSeconds(0.12f);
        animator.SetBool("IsPunch", false);
        PunchHitBox.SetActive(false);
        isPunching = false;

        yield return new WaitForSeconds(1f); // cooldown
        canPunch = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyPunch"))
        {
            TakeDamage(10);
        }

        if (other.gameObject.CompareTag("Water"))
        {
            canPunch = false;
            controller.Grounded = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            canPunch = true;
            controller.Grounded = true;
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
