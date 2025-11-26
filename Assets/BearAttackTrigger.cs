using UnityEngine;

public class BearAttackTrigger : MonoBehaviour
{
    private Bear bear;

    private void Start()
    {
        bear = GetComponentInParent<Bear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Stats player = other.GetComponent<Player_Stats>();
            if (player != null)
            {
                player.TakeDamage(bear.currentAttackDamage);
                Debug.Log("Oso hizo " + bear.currentAttackDamage + " de daño");
            }
        }
    }
}
