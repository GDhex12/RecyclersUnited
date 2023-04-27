using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private int timerSeconds;
    [SerializeField] private int neededLevel;
    private readonly TMP_Text timerText;
    private readonly TMP_Text availabilityText;
    private readonly string[] rectangleIcons = new string[2] {"BTN_BLUE_RECT_OUT", "BTN_GRAY_RECT_OUT"};
    private Sprite greyRectangle;
    [SerializeField] private string[] setMessages = new string[3] 
    {"Available!",
     "Unlocked at \nlevel ",
     "Available after "};
    [SerializeField] private ExperienceStats refToExperience;
    [SerializeField] private VolunteerCountManager refToVolunteers;
    [SerializeField] private Button useButton;

    private void Awake()
    {
        if (timerSeconds == 0) Debug.LogError(string.Format("Please set the timerSeconds variable in {0}!", gameObject.name));
        if (neededLevel == 0) Debug.LogError(string.Format("Please set the neededLevel variable in {0}!", gameObject.name));
        timerText.text = Convert.ToString(timerSeconds);
        if (neededLevel > refToExperience.level)
        {
            availabilityText.text = setMessages[1];
        }
        else availabilityText.text = string.Format("{0}{1}", setMessages[2], neededLevel);
    }

    private void ChangeButtonStatus(bool enabled)
    {
        if (enabled == false)
        {
            useButton.interactable = false;
            useButton.image = 
        } 
        else 
    }

    private void InitiateSpeedPower()
    {
        ChangeButtonStatus(false);
        refToVolunteers.IncreaseVolunteersSpeed();
        InitiateTimer();
    }

    private void InitiateMoreVolunteers()
    {
        ChangeButtonStatus(false);
        refToVolunteers.AddVolunteersTemporary(3, timerSeconds);
        InitiateTimer();
    }

    private void InitiateTimer()
    {

    }

    void Update()
    {
        
    }
}
