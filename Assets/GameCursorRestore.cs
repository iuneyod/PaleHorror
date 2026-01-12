using UnityEngine;
using StarterAssets;

public class GameCursorRestore : MonoBehaviour
{
    void Start()
    {
        StarterAssetsInputs inputs = FindObjectOfType<StarterAssetsInputs>();

        if (inputs != null)
        {
            inputs.cursorLocked = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
