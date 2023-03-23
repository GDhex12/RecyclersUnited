using Firebase.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushNotificationTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log(string.Format("Received registration token: {0}", token.Token));
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs message)
    {
        Debug.Log(string.Format("Received a message from: {0}", message.Message.From));
    }

}
