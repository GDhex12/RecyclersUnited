using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    DateTime exitTime;
    DateTime startTime;
    string prefName;

    public DateTime GetExitTime() { return exitTime; }
    public DateTime GetStartTime() { return startTime; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        prefName = $"{SceneManager.GetActiveScene().name}_time";
        startTime = DateTime.Now;
        GetLastExitTime();
    }

    public float TimeDiffBtwExitAndStart()
    {
        var diff = startTime.Subtract(exitTime);
        return (float)diff.TotalSeconds;
    }

    private void OnApplicationPause()
    {
        SaveExitTime();
    }

    private void OnDestroy()
    {
        SaveExitTime();
    }

    void SaveExitTime()
    {
        exitTime = DateTime.Now;
        PlayerPrefs.SetString(prefName, exitTime.ToString());
    }

    void GetLastExitTime()
    {
        string defaultString = "no date saved";
        string prefDate = PlayerPrefs.GetString(prefName, defaultString);
        if(!prefDate.Equals(defaultString) )
        {
            exitTime = DateTime.Parse(prefDate);
        }
    }
}
