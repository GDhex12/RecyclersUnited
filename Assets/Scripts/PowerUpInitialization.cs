using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInitialization : MonoBehaviour
{
    [SerializeField] GameObject powerupTab;
    private PowerUpUI[] powerIpUIs;

    private void Awake()
    {
        powerIpUIs = powerupTab.GetComponentsInChildren<PowerUpUI>();
    }
    private void OnEnable()
    {
        for (int i = 0; i < powerIpUIs.Length; i++)
        {
            powerIpUIs[i].CheckNeededLevel();
        }
    }
}
