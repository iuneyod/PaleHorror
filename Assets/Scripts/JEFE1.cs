using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JEFE1 : MonoBehaviour
{
    public int vida = 300;
    public int vidaMax = 300;
    public int damage = 20;

    public Animator animator;

    public Transform player;

    public GameObject Antena;

    public BoxCollider myCollider;

    public Transform apararicionAntena;


    public GameObject SpawnEffect;
    public GameObject EnemyPrefab;
    public List<Transform> SpawnPoints;

    public GameObject PrimerAtackEffect;
    public GameObject PrimerAttackTrigger;

    public GameObject SegundoAttackIntroEffect;
    public GameObject SegundoAttackEffect;
    public GameObject SegundoAttackTrigger;

    public GameObject FrezzeEffect;
    public GameObject FrezzeTrigger;

    public bool puedeAtacar = false;
    public bool isAtacking = false;
    public bool CanFreezeAtack = false;
    public bool puedeSpawnear = false;

    JEFE1_Effects jefeEffects;

    private void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        jefeEffects = FindObjectOfType<JEFE1_Effects>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        SpawnEffect.SetActive(false);
        PrimerAtackEffect.SetActive(false);
        PrimerAttackTrigger.SetActive(false);
        SegundoAttackIntroEffect.SetActive(false);
        SegundoAttackEffect.SetActive(false);
        SegundoAttackTrigger.SetActive(false);
        FrezzeEffect.SetActive(false);
        FrezzeTrigger.SetActive(false);
        puedeSpawnear = true;
        puedeAtacar = true;
        CanFreezeAtack = true;
        StartCoroutine(StartSpawn());


        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (jefeEffects.yaSpawned == true)
        {
            Vector3 lookDir = player.position - transform.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 6f);
            }

            if (puedeAtacar == true)
            {
                int ramdomIndex = Random.Range(0, 2);

                if (ramdomIndex == 0)
                {
                    StartCoroutine(SegundoAttack());

                }
                else if (ramdomIndex == 1)
                {
                    StartCoroutine(PrimerAttack());
                }
            }

            if (puedeSpawnear == true)
            {
                SpawnEnemy();
            }

            if (isAtacking == false)
            {
                animator.SetBool("Idle", true);
            }

            if (CanFreezeAtack == true)
            {
                StartCoroutine(FreezeAttack());
            }
        }
    }

    void SpawnEnemy()
    {
        StartCoroutine(ActiveSpawn());
    }

    IEnumerator ActiveSpawn()
    {
        puedeSpawnear = false;

        if (vida >= 200)
        {
            int randomIndex = Random.Range(2, SpawnPoints.Count);
            Transform spawnPoints = SpawnPoints[randomIndex];
            StartCoroutine(ActiveSpawnEffect(spawnPoints));
        }

        if (vida < 200 && vida >= 100)
        {
            int randomIndex = Random.Range(4, SpawnPoints.Count);
            Transform spawnPoints = SpawnPoints[randomIndex];
            StartCoroutine(ActiveSpawnEffect(spawnPoints));

        }

        if (vida < 100)
        {
            int randomIndex = Random.Range(6, SpawnPoints.Count);
            Transform spawnPoints = SpawnPoints[randomIndex];
            StartCoroutine(ActiveSpawnEffect(spawnPoints));

        }

        yield return new WaitForSeconds(20f);
        puedeSpawnear = true;
    }

    IEnumerator ActiveSpawnEffect(Transform spawnPoints)
    {
        SpawnEffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        Instantiate(EnemyPrefab, spawnPoints.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        SpawnEffect.SetActive(false);
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(6f);
        puedeSpawnear = true;
    }

    IEnumerator EsperaEntreAtaques()
    {
        yield return new WaitForSeconds(6f);
        puedeAtacar = true;
    }





    IEnumerator FreezeAttack()
    {
        isAtacking = true;
        CanFreezeAtack = false;

        animator.SetBool("Idle", false);
        animator.SetTrigger("Attack");

        // Copiamos la lista para no modificar la original
        List<Transform> pointsCopy = new List<Transform>(SpawnPoints);

        // Mezclamos la lista
        for (int i = 0; i < pointsCopy.Count; i++)
        {
            Transform temp = pointsCopy[i];
            int randomIndex = Random.Range(i, pointsCopy.Count);
            pointsCopy[i] = pointsCopy[randomIndex];
            pointsCopy[randomIndex] = temp;
        }

        // Tomamos los primeros 3
        for (int i = 0; i < 6; i++)
        {
            Transform spawnPoint = pointsCopy[i];

            GameObject effect = Instantiate(FrezzeEffect, spawnPoint.position, Quaternion.identity);
            GameObject trigger = Instantiate(FrezzeTrigger, spawnPoint.position, Quaternion.identity);

            effect.SetActive(true);
            trigger.SetActive(true);

            Destroy(effect, 5f);
            Destroy(trigger, 5f);
        }

        yield return new WaitForSeconds(5f);

        isAtacking = false;
        yield return new WaitForSeconds(4f);
        CanFreezeAtack = true;
    }



    IEnumerator PrimerAttack()
    {
        isAtacking = true;
        animator.SetBool("Idle", false);
        puedeAtacar = false;
        animator.SetTrigger("AttackSpike");

        List<Transform> pointsCopy = new List<Transform>(SpawnPoints);

        for (int i = 0; i < pointsCopy.Count; i++)
        {
            int randomIndex = Random.Range(i, pointsCopy.Count);
            Transform temp = pointsCopy[i];
            pointsCopy[i] = pointsCopy[randomIndex];
            pointsCopy[randomIndex] = temp;
        }

        // PRIMERO: efecto visual
        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = pointsCopy[i];
            GameObject effect = Instantiate(PrimerAtackEffect, spawnPoint.position, Quaternion.identity);
            effect.SetActive(true);
            Destroy(effect, 3.2f);
        }

        // ESPERAMOS 2 SEGUNDOS
        yield return new WaitForSeconds(1.6f);

        // DESPUÉS: trigger de daño
        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = pointsCopy[i];
            GameObject trigger = Instantiate(PrimerAttackTrigger, spawnPoint.position, Quaternion.identity);
            trigger.SetActive(true);
            Destroy(trigger, 2.5f);
        }

        yield return new WaitForSeconds(1f);

        isAtacking = false;
        StartCoroutine(EsperaEntreAtaques());
    }


    IEnumerator SegundoAttack()
    {
        isAtacking = true;
        animator.SetBool("Idle", false);
        puedeAtacar = false;
        animator.SetTrigger("AttackSpike");

        List<Transform> pointsCopy = new List<Transform>(SpawnPoints);

        for (int i = 0; i < pointsCopy.Count; i++)
        {
            int randomIndex = Random.Range(i, pointsCopy.Count);
            Transform temp = pointsCopy[i];
            pointsCopy[i] = pointsCopy[randomIndex];
            pointsCopy[randomIndex] = temp;
        }

        // PRIMERO: Intro Effect
        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = pointsCopy[i];
            GameObject intro = Instantiate(SegundoAttackIntroEffect, spawnPoint.position, Quaternion.identity);
            intro.SetActive(true);
            Destroy(intro, 4f);
        }

        // ESPERAMOS 2 SEGUNDOS
        yield return new WaitForSeconds(2f);

        // APARECE EL EFFECT
        List<GameObject> effects = new List<GameObject>();

        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = pointsCopy[i];
            GameObject effect = Instantiate(SegundoAttackEffect, spawnPoint.position, Quaternion.identity);
            effect.SetActive(true);
            effects.Add(effect);
            Destroy(effect, 2.5f);
        }

        // ESPERAMOS 0.8s ANTES DEL TRIGGER
        yield return new WaitForSeconds(0.5f);

        // APARECE EL TRIGGER
        for (int i = 0; i < 8; i++)
        {
            Transform spawnPoint = pointsCopy[i];
            GameObject trigger = Instantiate(SegundoAttackTrigger, spawnPoint.position, Quaternion.identity);
            trigger.SetActive(true);
            Destroy(trigger, 2.5f);
        }

        yield return new WaitForSeconds(1f);

        isAtacking = false;
        StartCoroutine(EsperaEntreAtaques());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaAttack"))
        {
            TakeDamage(5);
            animator.SetTrigger("Hit1");
        }
    }




    void TakeDamage(int damageAmount)
    {
        vida -= damageAmount;
        if (vida <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Antena.transform.position = apararicionAntena.position;
        animator.SetBool("Idle", false);
        animator.ResetTrigger("Attack");
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 4f);
    }

}
