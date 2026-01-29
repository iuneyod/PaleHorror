using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMinigame3 : MonoBehaviour
{
    MINIGAME3 minigame3;
    public Animator animator;
    public GameObject target;
    public NavMeshAgent agent;


    public float rangoVision = 12f;
    public float walkSpeed = 3f;
    public float runSpeed = 4f;
    public float patrolRadius = 100f;
    public float patrolMinRadius = 20f;
    public float patrolTime = 20f;
    public bool enRage = false;

    public bool vaMatar = false;


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

            animator.SetBool("IsRun", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            agent.speed = walkSpeed;
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

        if (rangoVision <= 12)
        {
            rangoVision = 12;
        }
        if (rangoVision > 12 && minigame3.linternActive == false)
        {
            rangoVision -= Time.deltaTime * 0.015f;
        }

        rangoVision += minigame3.ruido * Time.deltaTime * 0.5f;
    }

    public IEnumerator LinternaEnEnemigo()
    {
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
