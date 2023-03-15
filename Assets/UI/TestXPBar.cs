using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestXPBar : MonoBehaviour
{
    int level;
    float experience;
    int experienceNeededToLevelUp;

    public Slider levelUpBar;
    public TextMeshProUGUI currentLevel;

    void Start()
    {
        level = 0;
        experience = 0;
        experienceNeededToLevelUp = 10;

        levelUpBar.value = experience;
        levelUpBar.maxValue = experienceNeededToLevelUp;

        currentLevel.text = "Level : 0";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            experience += 0.7f;
            levelUpBar.value = experience;
        }

        if (levelUpBar.value >= levelUpBar.maxValue)
        {
            IncreaseLevel();
        }
    }

    void IncreaseLevel()
    {
        experience = 0;
        levelUpBar.value = experience;

        experienceNeededToLevelUp += 10;
        levelUpBar.maxValue = experienceNeededToLevelUp;

        level += 1;
        currentLevel.text = "Level : " + level.ToString();
    }
}
