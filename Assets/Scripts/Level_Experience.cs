using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level_Experience : MonoBehaviour
{
    public int level;
    public int experience;
    public int experienceToNextLevel;
    public float experienceMultiplier = 1.2f;
    public delegate void OnLevelUp(int newLevel);
    public event OnLevelUp onLevelUp;

    public TMP_Text levelText;
    public TMP_Text experienceText;
    public TMP_Text experienceToNextLevelText;

    public bool isLevel1 = false;
    public bool isLevel2 = false;
    public bool isLevel3 = false;
    public bool isLevel4 = false;
    public bool isLevel5 = false;
    public bool isLevel6 = false;
    public bool isLevel7 = false;
    public bool isLevel8 = false;
    public bool isLevel9 = false;
    public bool isLevel10 = false;

    Inventario inventario;
    void Start()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = CalculateExperienceToNextLevel();
        UpdateUI();

        inventario = FindObjectOfType<Inventario>();
    }

    void Update()
    {
        UpdateUI();

        if(inventario.inventarioAbierto == true)
        {
            levelText.enabled = false;
            experienceText.enabled = false;
            experienceToNextLevelText.enabled = false;
        }
        else
        {
            levelText.enabled = true;
            experienceText.enabled = true;
            experienceToNextLevelText.enabled = true;
        }
    }

    void UpdateUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + level;
        if (experienceText != null)
            experienceText.text = "EXP: " + experience;
        if (experienceToNextLevelText != null)
            experienceToNextLevelText.text = "/ "+ experienceToNextLevel;
    }

    public void AddExperience(int amount)
    {
        experience += Mathf.RoundToInt(amount * experienceMultiplier);
        while (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        experience -= experienceToNextLevel;
        level++;
        experienceToNextLevel = CalculateExperienceToNextLevel();
        onLevelUp?.Invoke(level);

        UpdateLevelBools();
    }

    void UpdateLevelBools()
    {
        isLevel1 = (level == 1);
        isLevel2 = (level == 2);
        isLevel3 = (level == 3);
        isLevel4 = (level == 4);
        isLevel5 = (level == 5);
        isLevel6 = (level == 6);
        isLevel7 = (level == 7);
        isLevel8 = (level == 8);
        isLevel9 = (level == 9);
        isLevel10 = (level == 10);
    }



    int CalculateExperienceToNextLevel()
    {
        return Mathf.RoundToInt(100 * Mathf.Pow(1.5f, level - 1));
    }
}
