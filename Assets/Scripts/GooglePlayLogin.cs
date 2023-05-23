using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.InputSystem;

public class GooglePlayLogin : MonoBehaviour
{
    public static GooglePlayLogin Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayGamesPlatform.Activate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void ManuallyAuthenticate()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
        }
        else
        {
            Debug.Log(status);
        }
    }
    public void PostLeaderboardLevelToGPS(int level)
    {
        Social.ReportScore(level, GPGSIds.leaderboard_level, (bool success) => {
            // handle success or failure
        });
    }
    #region GPGS_UI
    public void ShowLeaderboard()
    {
        // show leaderboard UI
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_level);
    }
    public void ShowAchievements()
    {
        // show achievement UI
        Social.ShowAchievementsUI();
    }
    #endregion

    #region AchievementManaging

    public void UnlockAchievement(string achievement)
    {
        Social.ReportProgress(achievement, 100.0f, (bool success) => {
            // handle success or failure
        });
    }
    public void IncrementAchievement(string achievement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        achievement, 1, (bool success) => {
            // handle success or failure
        });
    }

    #endregion
}
