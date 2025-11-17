using System.Collections;
using UnityEngine;

public class Comon_Mutant : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int damage = 10;
    public Animator animator;
    public int Speed = 3;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public Transform player;
    public ParticleSystem ParticleSystem;
    public GameObject AttackTrigger;

    public bool HasAxe = false;
    public bool attacking = false;
    private bool isDead = false;
    private bool canAttack = true;
    private Coroutine attackRoutine;

    private void Start()
    {
        if (ParticleSystem) ParticleSystem.Stop();
    }

    void Update()
    {
        if (isDead) return;

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

        // cooldown de 1.5 s antes del siguiente ataque
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
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        AttackTrigger.SetActive(false);
        animator.SetTrigger("IsDead");

        if (ParticleSystem) ParticleSystem.Play();
        tag = "Untagged";
        Destroy(gameObject, 4f);
    }
}
