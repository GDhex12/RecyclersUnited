using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceStats : MonoBehaviour
{
    public RectTransform refToExpBack;
    public TMP_Text refToExpValues;
    public TMP_Text refToLevelValue;

    public float requiredAmountToNextLv;
    public int level = 3;
    public float experienceToIncrease = 0f;

    private float expPrevious = 0f;
    private const float expBarWidth = 295f;

    private void Awake()
    {
        requiredAmountToNextLv = (float)((level / 10 + level % 10) * 100 * Math.Pow(10, level / 10));
    }

    // Start is called before the first frame update
    void Start()
    {
        refToLevelValue.text = Convert.ToString(level);
        refToExpValues.text = string.Format("{0} / {1}", expPrevious, requiredAmountToNextLv);
        expPrevious = experienceToIncrease;
        if (experienceToIncrease != 0)
        {
            refToExpBack.sizeDelta = new Vector2(CalculateExpBarDifference(experienceToIncrease), refToExpBack.sizeDelta.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (expPrevious != experienceToIncrease)
        {
            expPrevious = experienceToIncrease;
            refToExpValues.text = string.Format("{0} / {1}", expPrevious, requiredAmountToNextLv);
            UpdateExpPoints();
        }
    }

    private void UpdateExpPoints()
    {
        // Level up
        if (experienceToIncrease >= requiredAmountToNextLv)
        {
            experienceToIncrease -= requiredAmountToNextLv;
            refToExpBack.sizeDelta = new Vector2(CalculateExpBarDifference(experienceToIncrease - requiredAmountToNextLv), refToExpBack.sizeDelta.y);
            level++;
            refToLevelValue.text = Convert.ToString(level);
        }
        else
        {
            Debug.Log(string.Format("Player is now at {0} experience points", experienceToIncrease));
            refToExpBack.sizeDelta = new Vector2(CalculateExpBarDifference(experienceToIncrease), refToExpBack.sizeDelta.y);
        }
    }

    private float CalculateExpBarDifference(float difference)
    {
        float expBarSize = 295f;
        expBarSize -= (difference / requiredAmountToNextLv) * expBarWidth;
        return expBarSize;
    }
}
