using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameCursorRestore : MonoBehaviour
{
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Cambia esto por el nombre real de tu menú
        if (sceneName == "MenuPrincipal")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        if (sceneName == "MainScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // En escenas jugables
        StarterAssetsInputs inputs = FindObjectOfType<StarterAssetsInputs>();

        if (inputs != null)
        {
            inputs.cursorLocked = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
