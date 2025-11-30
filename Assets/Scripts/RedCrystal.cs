using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystal : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool RedCrystal_Desbloqueado = false;

    Player_Stats playerStats;

    private void Start()
    {
        Particulas.SetActive(false);
        playerStats = FindObjectOfType<Player_Stats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enArea = false;
        }
    }

    private void Update()
    {
        Coger();
    }


    public void Coger()
    {
        if (enArea == true && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MostrarParticulas());
        }
    }

    public IEnumerator MostrarParticulas()
    {
        Particulas.SetActive(true);
        yield return new WaitForSeconds(2f);
        Particulas.SetActive(false);
        RedCrystal_Desbloqueado = true;
        playerStats.canSpecialAttack2 = true;
        Destroy(gameObject);
    }
}
