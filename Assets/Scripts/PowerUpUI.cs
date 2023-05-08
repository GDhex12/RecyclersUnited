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
     "Unlocks at \nlevel ",
     "Active!"};
    [SerializeField] private Color32[] textColor = 
    {
        new Color32(0x00, 0x7B, 0x2F, 0xFF), // Dark Green
        new Color32(0xE5, 0x0B, 0x00, 0xFF)  // Dark Red
    };
    [SerializeField] private VolunteerCountManager refToVolunteers;
    [SerializeField] private Button useButton;
    [SerializeField] private bool isPowerUpActive = false;
    private FunctionTimer refToTimer;

    private void Awake()
    {
        if (timerSeconds == 0) Debug.LogError(string.Format("Please set the timerSeconds variable in {0}!", gameObject.name));
        if (neededLevel == 0) Debug.LogError(string.Format("Please set the neededLevel variable in {0}!", gameObject.name));
        timerText.text = Convert.ToString(timerSeconds);
        CheckNeededLevel();
    }
    public void CheckNeededLevel()
    {
        if (neededLevel <= PersistantData.Instance.playerData.Level)
        {
            availabilityText.text = setMessages[0];
            availabilityText.color = textColor[0];
            ChangeButtonStatus(true);
        }
        else
        {
            availabilityText.text = string.Format("{0}{1}", setMessages[1], neededLevel);
            availabilityText.color = textColor[1];
            ChangeButtonStatus(false);
        }
    }

    private void ChangeButtonStatus(bool enabled)
    {
        if (enabled == false)
        {
            useButton.interactable = false;
            useButton.image.sprite = PressableButtons.i.grayButton;
        } 
        else
        {
            availabilityText.text = setMessages[0];
            availabilityText.color = textColor[0];
            useButton.interactable = true;
            useButton.image.sprite = PressableButtons.i.blueButton;
        }
    }

    public void InitiateSpeedPower()
    {
        ChangeButtonStatus(false);
        refToVolunteers.IncreaseVolunteersSpeed();
        refToTimer = FunctionTimer.Create(() => ChangeButtonStatus(true), timerSeconds);
        availabilityText.text = setMessages[2];
        availabilityText.color = textColor[1];
        isPowerUpActive = true;
    }

    public void InitiateMoreVolunteers()
    {
        ChangeButtonStatus(false);
        refToVolunteers.AddVolunteersTemporary(3, timerSeconds);
        refToTimer = FunctionTimer.Create(() => ChangeButtonStatus(true), timerSeconds);
        availabilityText.text = setMessages[2];
        availabilityText.color = textColor[1];
        isPowerUpActive = true;
    }

    private void Update()
    {
        if (isPowerUpActive)
        {
            string timeLeft = Convert.ToString((int)refToTimer.GetTimeLeft());
            if (Convert.ToInt32(timeLeft) == 0)
            {
                timerText.text = Convert.ToString(timerSeconds);
                ChangeButtonStatus(true);
                refToTimer = null;
                isPowerUpActive = false;
            }
            else timerText.text = timeLeft;
        }
    }
}
