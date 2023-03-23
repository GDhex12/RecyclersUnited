using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

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
}
