using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public int health = 120;
    public int maxHealth = 120;
    public int damage = 15;
    public Animator animator;
    public int Speed = 4;
    public float chaseRange = 15f;
    public float attackRange = 2.2f;
    public Transform player;
    public ParticleSystem ParticleSystem;
    public GameObject AttackTrigger;
    public int OsosMuertos = 0;
    public bool freezeRotation = false;
    public int currentAttackDamage = 0;


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

        if (!freezeRotation)
        {
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 6f);
            }
        }


        float distance = Vector3.Distance(transform.position, player.position);

        if (!attacking)
        {
            if (distance < chaseRange && distance > attackRange)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                transform.position += dir * Speed * Time.deltaTime;

                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

                animator.SetBool("Run Forward", true);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Run Forward", false);
            }

            if (distance <= attackRange && canAttack)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        freezeRotation = true;
        canAttack = false;
        attacking = true;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Buff");
        animator.ResetTrigger("Get Hit Front");
        animator.SetBool("Eat", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Sleep", false);
        animator.SetBool("Run Forward", false);
        animator.SetBool("Idle", false);


        int dropChance = UnityEngine.Random.Range(1, 5);

        // Ataque 1
        if (dropChance == 1)
        {
            animator.SetTrigger("Attack1");
            currentAttackDamage = damage;
        }

        // Ataque 2
        else if (dropChance == 2)
        {
            animator.SetTrigger("Attack2");
            currentAttackDamage = damage;
        }

        // Ataque 3
        else if (dropChance == 3)
        {
            animator.SetTrigger("Attack3");
            currentAttackDamage = damage;
        }

        // Ataque 5 (especial)
        else if (dropChance == 4)
        {
            animator.SetTrigger("Attack5");
            currentAttackDamage = damage + 5;   //daño extra solo aquí
        }



        yield return new WaitForSeconds(0.4f);
        AttackTrigger.SetActive(true);
   
        yield return new WaitForSeconds(0.05f);
        AttackTrigger.SetActive(false);

        yield return new WaitForSeconds(1.2f);
        StartCoroutine(QuickTurnToPlayer());

        attacking = false;
        freezeRotation = false;


        yield return new WaitForSeconds(1.5f);
        canAttack = true;
        attackRoutine = null;
    }

    IEnumerator QuickTurnToPlayer()
    {
        float duration = 0.3f;     // tiempo que tarda en girar
        float timer = 0f;

        while (timer < duration && player != null)
        {
            Vector3 dir = player.position - transform.position;
            dir.y = 0;

            if (dir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 12f);
                // 12f es una velocidad super rápida, pero suave
            }

            timer += Time.deltaTime;
            yield return null;
        }
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
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack3");
            animator.ResetTrigger("Attack5");

            animator.SetTrigger("Get Hit Front");
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
            animator.ResetTrigger("Attack1");
            animator.ResetTrigger("Attack2");
            animator.ResetTrigger("Attack3");
            animator.ResetTrigger("Attack5");

            animator.SetTrigger("Get Hit Front");
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
        if (isDead) return;
        isDead = true;

        OsosMuertos++;
        levelExperience.AddExperience(2500);

        // Detener ataques
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        AttackTrigger.SetActive(false);

        // Evitar que otras animaciones interfieran
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Attack5");
        animator.ResetTrigger("Get Hit Front");

        animator.SetBool("Run Forward", false);
        animator.SetBool("Idle", false);
        animator.SetTrigger("Death");

        // Quitar colisiones
        tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("NoColision");

        // Iniciar corrutina completa
        StartCoroutine(DeathSequence());

        // --- DROP ---
        if (puedeDropear && inventario.Huesos < 5)
        {
            int dropChance = UnityEngine.Random.Range(0, 100);
            if (dropChance < 99)
            {
                inventario.Huesos += 1;
                Debug.Log("DROP HUESO");
            }
        }
    }

    IEnumerator DeathSequence()
    {
        // Partículas
        if (ParticleSystem)
            ParticleSystem.Play();

        // Esperar que la animación de muerte se vea completa
        yield return new WaitForSeconds(2.5f);

        if (ParticleSystem)
            ParticleSystem.Stop();

        // Esperar un poco más por seguridad
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }



    public IEnumerator DestroyAfterDelay()
    {
        ParticleSystem.Play();
        yield return new WaitForSeconds(1.2f);
        ParticleSystem.Stop();
    }
}
