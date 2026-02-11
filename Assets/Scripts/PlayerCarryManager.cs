using UnityEngine;

public class PlayerCarryManager : MonoBehaviour
{
    public static PlayerCarryManager Instance;

    public bool tieneObjeto = false;

    private void Awake()
    {
        Instance = this;
    }
}
