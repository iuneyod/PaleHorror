using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMainScene : MonoBehaviour
{
    ArcadeMachine arcadeMachine;
    StarterAssetsInputs starterAssetsInputs;
    ThirdPersonController controller;
    public GameObject arcadeUI;
    public Button dinoButton;
    public Button dinoButtonBlock;
    public Button blockGame;
    public Button blockGameBlock;
    public bool puedeJugarDino = false;
    public bool puedeJugarBlock = false;
    public TMP_Text dinoBlockText;
    public TMP_Text gameBlockTextBlock;
    private bool puedoInteractuarDinoBlock = true;
    private bool puedoInteractuarGameBlock = true;

    void Start()
    {
        arcadeUI.SetActive(false);
        arcadeMachine = FindObjectOfType<ArcadeMachine>();
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        controller = FindObjectOfType<ThirdPersonController>();
    }

    private void Update()
    {
        if (puedeJugarDino)
        {
            dinoButton.gameObject.SetActive(true);
            dinoButtonBlock.gameObject.SetActive(false);
        }
        else
        {
            dinoButton.gameObject.SetActive(false);
            dinoButtonBlock.gameObject.SetActive(true);
        }

        if (puedeJugarBlock)
        {
            blockGame.gameObject.SetActive(true);
            blockGameBlock.gameObject.SetActive(false);
        }
        else
        {
            blockGame.gameObject.SetActive(false);
            blockGameBlock.gameObject.SetActive(true);
        }

        if (arcadeMachine.pantallaAbierta == true)
        {
            starterAssetsInputs.cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
            controller.MoveSpeed = 0f;
            controller.SprintSpeed = 0f;
        }
        else
        {
            starterAssetsInputs.cursorInputForLook = true;
            Cursor.lockState = CursorLockMode.Locked;
            controller.MoveSpeed = 5f;
            controller.SprintSpeed = 8f;
        }
    }

    public void DinoFall()
    {
        SceneManager.LoadScene("ArcadeMinigame");
    }

    public IEnumerator DinoFallBlockCorrutine()
    {
        if (!puedoInteractuarDinoBlock) yield break;
        puedoInteractuarDinoBlock = false;
        dinoBlockText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        float tiempoDesaparecer = 2f;
        float timer = 0f;
        while (timer < tiempoDesaparecer)
        {
            dinoBlockText.alpha = Mathf.Lerp(1f, 0f, timer / tiempoDesaparecer);
            timer += Time.deltaTime;
            yield return null;
        }
        dinoBlockText.gameObject.SetActive(false);
        puedoInteractuarDinoBlock = true;
    }

    public void DinoFallBlock()
    {
        StartCoroutine(DinoFallBlockCorrutine());
    }

    public void BlockGame()
    {
        //SceneManager.LoadScene("BlockMinigame");
    }

    public IEnumerator BlockGameBlockCorrutine()
    {
        if (!puedoInteractuarGameBlock) yield break;
        puedoInteractuarGameBlock = false;
        gameBlockTextBlock.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        float tiempoDesaparecer = 2f;
        float timer = 0f;
        while (timer < tiempoDesaparecer)
        {
            gameBlockTextBlock.alpha = Mathf.Lerp(1f, 0f, timer / tiempoDesaparecer);
            timer += Time.deltaTime;
            yield return null;
        }
        gameBlockTextBlock.gameObject.SetActive(false);
        puedoInteractuarGameBlock = true;
    }

    public void BlockGameBlock()
    {
        StartCoroutine(BlockGameBlockCorrutine());
    }
}
