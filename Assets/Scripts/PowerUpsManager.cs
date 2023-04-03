using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "PowerUp")
                {
                    hit.transform.gameObject.GetComponent<PowerUp>().OnClick();
                    PowerUpType type = hit.transform.gameObject.GetComponent<PowerUp>().GetType();
                    if (type == PowerUpType.Add)
                    {
                        countManager.AddVolunteersTemporary(3, 10f);
                    }
                    else if (type == PowerUpType.Speed)
                    {
                        countManager.IncreaseVolunteersSpeed();
                    }


                }
            }
        }


    }

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
