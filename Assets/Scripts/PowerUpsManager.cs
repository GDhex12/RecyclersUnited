using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] private VolunteerCountManager countManager;
    [SerializeField] private SpawnObject spawnObject;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Button speedUpButton;
    [SerializeField] private float speedUpButtonCoolDown;
    [SerializeField] private Color disabledButtonColor;
    [SerializeField] private Color enabledButtonColor;
    private bool helicopterClicked = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                if (hit.transform.CompareTag("Crate"))
                {
                    hit.transform.gameObject.GetComponent<OnClickAddCoins>().OnClick();
                    UnlockCrateAchievements();
                }
                else if (hit.transform.CompareTag("Helicopter") && !helicopterClicked)
                {
                    helicopterClicked = true;
                    hit.transform.root.GetComponent<Animator>().SetTrigger("MoveToExit");
                    countManager.AddVolunteersTemporary(3, 10f);
                    FunctionTimer.Create(()=>{ helicopterClicked = false; },2f);
                    UnlockPowerupAchievements();

                }
                else if (hit.transform.CompareTag("Plane") && !helicopterClicked)
                {
                    helicopterClicked = true;
                    countManager.IncreaseVolunteersSpeed();
                    FunctionTimer.Create(() => { helicopterClicked = false; }, 2f);
                    UnlockPowerupAchievements();
                }

            }
        }


    }
    #region AchivementUnlocking
    private void UnlockCrateAchievements()
    {
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_crate_collector_1);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_crate_collector_2);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_crate_collector_3);
    }
    private void UnlockPowerupAchievements()
    {
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_1);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_2);
        GooglePlayLogin.Instance.IncrementAchievement(GPGSIds.achievement_hands_on_3);
    }
    #endregion
    public void SpeedUpVolunteers()
    {
        speedUpButton.interactable = false;
        countManager.IncreaseVolunteersSpeed();
        FunctionTimer.Create(EnableSpeedUpButton, speedUpButtonCoolDown);
    }

    private void EnableSpeedUpButton()
    {
        speedUpButton.interactable = true;
    }

   
}
