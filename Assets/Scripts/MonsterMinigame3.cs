using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMinigame3 : MonoBehaviour
{
    MINIGAME3 minigame3;
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;


    [Header("Valores base")]
    public float rangoVision;
    public float walkSpeed;
    public float runSpeed;

    public float rangoVisionNormal = 10f;
    public float rangoVisionDificil = 12f;

    public float walkSpeedNormal = 2.5f;
    public float runSpeedNormal = 3.5f;

    public float walkSpeedDificil = 3f;
    public float runSpeedDificil = 4f;

    float rangoVisionBase;
    public float patrolRadius = 100f;
    public float patrolMinRadius = 20f;
    public float patrolTime = 20f;
    public bool enRage = false;

    public bool vaMatar = false;

    public AudioSource MonsterLinterna;
    public AudioClip gritoLinterna;

    public AudioSource Persecucion;


    Vector3 patrolCenter;
    bool tieneDestino = false;

    float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        minigame3 = FindObjectOfType<MINIGAME3>();
        target = GameObject.Find("Player");

        agent.updateRotation = false;
        patrolCenter = transform.position;

        ConfigurarDificultad();
    }

    void ConfigurarDificultad()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.CompletoTodosLosMinijuegos)
        {
            // MODO DIFÍCIL
            rangoVisionBase = rangoVisionDificil;
            walkSpeed = walkSpeedDificil;
            runSpeed = runSpeedDificil;
        }
        else
        {
            // MODO NORMAL
            rangoVisionBase = rangoVisionNormal;
            walkSpeed = walkSpeedNormal;
            runSpeed = runSpeedNormal;
        }

        rangoVision = rangoVisionBase;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && enRage == false && minigame3.muriendo == false)
        {
            StartCoroutine(minigame3.Muerte());
        }
    }

    void Update()
    {
        if (enRage) return;

        if (!agent.isOnNavMesh) return;

        float distancia = Vector3.Distance(transform.position, target.transform.position);

        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 8f);
        }


        if (distancia <= rangoVision)
        {
            // PERSEGUIR
            agent.speed = runSpeed;
            agent.SetDestination(target.transform.position);
            if (!Persecucion.isPlaying && vaMatar == false)
                Persecucion.Play();
            animator.SetBool("IsRun", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            if (Persecucion.isPlaying)
                Persecucion.Stop(); agent.speed = walkSpeed;
            animator.SetBool("IsRun", false);
            animator.SetBool("isWalking", true);

            timer += Time.deltaTime;

            if (!tieneDestino || timer >= patrolTime || agent.remainingDistance < 1.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
                randomDirection.y = 0;

                Vector3 puntoLejano = patrolCenter + randomDirection;

                if (Vector3.Distance(transform.position, puntoLejano) < patrolMinRadius)
                {
                    puntoLejano = patrolCenter + randomDirection.normalized * patrolRadius;
                }

                if (NavMesh.SamplePosition(puntoLejano, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                    tieneDestino = true;
                    timer = 0f;
                }
            }
        }

        // PARTIMOS DEL VALOR BASE
        rangoVision = rangoVisionBase;

        //ruido (escala suave)
        float ruidoFactor = minigame3.ruido / minigame3.ruidoMaximo;
        rangoVision += ruidoFactor * 4f; // AJUSTA 4f a tu gusto

        // linterna
        if (minigame3.linternActive)
        {
            rangoVision += 2f;
        }

        // mínimo
        if (rangoVision < rangoVisionBase)
        {
            rangoVision = rangoVisionBase;
        }

        // linterna
        if (rangoVision > rangoVisionBase && !minigame3.linternActive)
        {
            rangoVision -= Time.deltaTime * 0.015f;
        }

        // ruido
        rangoVision += minigame3.ruido * Time.deltaTime * 0.5f;
    }

    public IEnumerator LinternaEnEnemigo()
    {
        MonsterLinterna.PlayOneShot(gritoLinterna);
        enRage = true;
        minigame3.duracionLinterna -= 5f;

        agent.isStopped = true;
        agent.ResetPath();

        animator.SetBool("IsRun", false);
        animator.SetBool("isWalking", false);
        animator.ResetTrigger("Rage");
        animator.SetTrigger("Rage");

        yield return new WaitForSeconds(1f);

        agent.isStopped = false;
        enRage = false;
        minigame3.puedeUsarLinterna = false;
        minigame3.lintern.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        minigame3.lintern.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        minigame3.lintern.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        minigame3.lintern.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        minigame3.lintern.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        minigame3.lintern.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        minigame3.lintern.SetActive(false);
        minigame3.puedeUsarLinterna = true;
        minigame3.linternActive = false;
    }

}
