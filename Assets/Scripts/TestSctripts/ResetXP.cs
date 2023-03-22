using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetXP : MonoBehaviour
{
    ExperienceStats stats;

    private void Awake()
    {
        stats = FindObjectOfType<ExperienceStats>();
    }

    public void ResetStats()
    {
        stats.SetExperience(1, 0, 100);
        stats.UpdateExpPoints();
    }
}
