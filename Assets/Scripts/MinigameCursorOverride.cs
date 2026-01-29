using UnityEngine;
using StarterAssets;

public class MinigameCursorOverride : MonoBehaviour
{
    void Start()
    {
        StarterAssetsInputs inputs = FindObjectOfType<StarterAssetsInputs>();

        if (inputs != null)
        {
            inputs.cursorLocked = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
