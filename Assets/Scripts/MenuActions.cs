using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public GameObject panel;
    public bool isopen;
    public void OpenExitMenu()
    {
        isopen = panel.activeSelf;

        if (!isopen)
        {
            isopen = true;
            panel.SetActive(true);
        }
        else if (isopen)
        {
            isopen = false;
            panel.SetActive(false);
        }
    }
    public void OpenLeaderboard()
    {
#if UNITY_ANDROID
        GooglePlayLogin.Instance.ShowLeaderboard();
#endif
    }
    public void OpenAchievements()
    {
#if UNITY_ANDROID
        GooglePlayLogin.Instance.ShowAchievements();
#endif
    }
}