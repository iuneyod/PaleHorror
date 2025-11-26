using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epic_Mutant : MonoBehaviour
{
    public int health = 80;
    public int maxHealth = 80;
    public int damage = 15;
    public Animator animator;
    public float Speed = 3.5f;
    public float chaseRange = 14f;
    public float attackRange = 2f;
    public Transform player;
    public ParticleSystem ParticleSystem;
    public GameObject AttackTrigger;
    public int MutanteEpicoMuertos = 0;

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
        MutanteEpicoMuertos++;
        levelExperience.AddExperience(140);

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
        if (puedeDropear && inventario.DienteEpico < 5)
        {
            int dropChance = UnityEngine.Random.Range(0, 100);

            if (dropChance < 99) // 99% chance
            {
                inventario.DienteEpico += 1;
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
