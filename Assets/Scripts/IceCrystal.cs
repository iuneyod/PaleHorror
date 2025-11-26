using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystal : MonoBehaviour
{
    public bool enArea = false;
    public GameObject Particulas;
    public bool IceCrystal_Desbloqueado = false;

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
        Mapa();
    }


    public void Mapa()
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
        IceCrystal_Desbloqueado = true;
        playerStats.canSpecialAttack1 = true;
        Destroy(gameObject);
    }
}
