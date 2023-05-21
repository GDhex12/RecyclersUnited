using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugWindow : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] GameObject debugWindow;
    [SerializeField] Key debugKey;

    // Start is called before the first frame update
    void Start()
    {
        debugWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[debugKey].wasPressedThisFrame)
        {
            debugWindow.SetActive(!debugWindow.activeInHierarchy);
        }
    }

    public void ResetTotalGarbageCount()
    {
        //if (GameObject.FindObjectOfType<TrashController>() != null)
        if (TrashController.Instance != null)
        {
            TrashController.Instance.ResetTotalGarbageCount();
        }
    }

    public void ResetStorageAmount()
    {
        Storage storage = GameObject.FindObjectOfType<Storage>();
        if (storage != null)
        {
            storage.SetGarbageCount(0);
        }
    }
    
    public void ResetVehicleAmount()
    {
        VehicleSystem vehicle = GameObject.FindObjectOfType<VehicleSystem>();
        if (vehicle != null)
        {
            vehicle.SetGarbageCount(0);
        }
    }

    public void GiveReward_Money(int moneyReward)
    {
        CurrencyManager.instance.AddCurrency(moneyReward);
        Debug.Log($"Rewarded ${moneyReward}");
    }

    public void GiveMoney(int amount)
    {
        CurrencyManager.instance.AddCurrency(amount);
    }
#endif
}
