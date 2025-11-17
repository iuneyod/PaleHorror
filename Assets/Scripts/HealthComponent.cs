using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;

    // Este método se usa para actualizar la barra
    public void SetHealth(int current, int max)
    {
        CurrentHealth = Mathf.Clamp(current, 0, max);
        MaxHealth = max;
    }
}
