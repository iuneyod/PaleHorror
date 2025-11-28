using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Comon_Mutant : MonoBehaviour
{
    public int health = 60;
    public int maxHealth = 60;
    public int damage = 10;
    public Animator animator;
    public int Speed = 3;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public Transform player;
    public ParticleSystem ParticleSystem;
    public GameObject AttackTrigger;
    public int MutanteComunMuertos = 0;

    public bool puedeDropear = true;          // Controlado por Inventario

    public bool HasAxe = false;
    public bool attacking = false;
    private bool isDead = false;
    private bool canAttack = true;
    private Coroutine attackRoutine;

    Inventario inventario;
    Level_Experience levelExperience;

    private void Start()
    {
        if (ParticleSystem) ParticleSystem.Stop();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        inventario = FindObjectOfType<Inventario>();
        levelExperience = FindObjectOfType<Level_Experience>();
    }

    void Update()
    {
        if (isDead) return;
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!attacking)
        {
            if (distance < chaseRange && distance > attackRange)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                transform.position += dir * Speed * Time.deltaTime;

                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

                animator.SetBool("IsRun", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("IsRun", false);
            }

            if (distance <= attackRange && canAttack)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        attacking = true;

        animator.ResetTrigger("Hit1");
        animator.ResetTrigger("IsDead");
        animator.ResetTrigger("Attack");
        animator.SetBool("IsRun", false);
        animator.SetBool("Idle", false);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);
        AttackTrigger.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        AttackTrigger.SetActive(false);

        yield return new WaitForSeconds(1.2f);
        attacking = false;

        yield return new WaitForSeconds(1.5f);
        canAttack = true;
        attackRoutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPunch") && !isDead)
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
                AttackTrigger.SetActive(false);
                attacking = false;
                canAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }

            TakeDamage(20);
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Hit1");
            StartCoroutine(GoIdle());

            if (HasAxe) TakeDamage(10);
        }
        if (other.CompareTag("SpecialEffect1") && !isDead)
        {
            StartCoroutine(SpeedVariant());
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
                AttackTrigger.SetActive(false);
                attacking = false;
                canAttack = false;
                StartCoroutine(ResetAttackCooldown());
            }

            TakeDamage(40);
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Hit1");
            StartCoroutine(GoIdle());
        }

        if (other.CompareTag("BalaAttack"))
        {
            TakeDamage(5);
            animator.SetTrigger("Hit1");

        }
    }

    public IEnumerator SpeedVariant()
    {
        Speed /= 2;
        yield return new WaitForSeconds(2f);
        Speed *= 2;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        if (!isDead)
            canAttack = true;
    }

    IEnumerator GoIdle()
    {
        yield return new WaitForSeconds(0.6f);
        if (!isDead)
            animator.SetBool("Idle", true);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        MutanteComunMuertos++;
        levelExperience.AddExperience(4500);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        AttackTrigger.SetActive(false);
        animator.SetTrigger("IsDead");

        StartCoroutine(DestroyAfterDelay());
        tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("NoColision");
        Destroy(gameObject, 4f);

        // --- DROP DE DIENTE ---
        if (puedeDropear && inventario.DienteComun < 5)
        {
            int dropChance = UnityEngine.Random.Range(0, 100);

            if (dropChance < 99) // 99% chance
            {
                inventario.DienteComun += 1;
                Debug.Log("DROP DIENTE");
            }
        }
    }

    public IEnumerator DestroyAfterDelay()
    {
        ParticleSystem.Play();
        yield return new WaitForSeconds(1.2f);
        ParticleSystem.Stop();
    }
}
