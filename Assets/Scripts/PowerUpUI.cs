using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private int timerSeconds;
    [SerializeField] private int neededLevel;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text availabilityText;
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
        if (neededLevel <= refToExperience.level)
        {
            availabilityText.text = setMessages[0];
        }
        else availabilityText.text = string.Format("{0}{1}", setMessages[1], neededLevel);
    }

    private void ChangeButtonStatus(bool enabled)
    {
        if (enabled == false)
        {
            useButton.interactable = false;
            useButton.image.sprite = PressableButtons.i.grayButton;
        } 
        //else 
    }

    public void InitiateSpeedPower()
    {
        ChangeButtonStatus(false);
        refToVolunteers.IncreaseVolunteersSpeed();
        InitiateTimer();
    }

    public void InitiateMoreVolunteers()
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
