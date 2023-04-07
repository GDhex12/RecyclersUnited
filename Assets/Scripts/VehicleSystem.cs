using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI vehicleUI;

    [Header("Vehicle capacity")]
    [SerializeField] private int maxGarbageCount = 25;
    public int currentGarbageCount = 0;

    [Header("Money")]
    [SerializeField] private int moneyPerGarbage = 10;


    private void Start()
    {
        GetParamsFromSave();
        UpdateGarbage();
    }

    public int GetGarbageCount()
    {
        return currentGarbageCount;
    }

    public void SetMaxGarbageCount(int count)
    {
        maxGarbageCount = count;
        UpdateGarbage();
    }

    public int GetMaxGarbageCount()
    {
        return maxGarbageCount;
    }


    public void AddGarbage(int amount)
    {
        if (IsFull())
            return;

        if (currentGarbageCount + amount > maxGarbageCount)
        {
            currentGarbageCount = maxGarbageCount;
        }
        else
        {
            currentGarbageCount += amount;
        }

        UpdateGarbage();
    }

    public void RemoveGarbage(int amount)
    {
        if (currentGarbageCount - amount <= 0)
        {
            currentGarbageCount = 0;
        }
        else
        {
            currentGarbageCount -= amount;
        }

        UpdateGarbage();
    }

    public int RemoveAllGarbage()
    {
        int amount = currentGarbageCount;
        currentGarbageCount = 0;
        UpdateGarbage();
        return amount;
    }

    public void LoadVehicle(Storage storage)
    {
        LoadVehicle(storage, 1);
    }

    public void LoadVehicle(Storage storage, int amount)
    {
        if (IsFull())
            return;

        if (!storage.IsEmpty() && amount <= storage.GetGarbageCount())
        {
            storage.RemoveGarbage(amount);
            AddGarbage(amount);
        }
    }

    public void ExchangeGarbageToMoney(int garbageCount)
    {
        CurrencyManager.instance.AddCurrency(garbageCount * moneyPerGarbage);
    }

    public bool IsFull()
    {
        return !(currentGarbageCount < maxGarbageCount);
    }

    void UpdateGarbage()
    {
        // Update storage UI
        if (vehicleUI != null)
        {
            vehicleUI.text = StorageStringFormat();
        }
        SaveParams();
    }

    string StorageStringFormat()
    {
        return $"{currentGarbageCount}/{maxGarbageCount}";
    }
    public void GetParamsFromSave()
    {
        //get parameters from savefile
        //maxGarbageCount
        //currentGarbageCount
        //mapID
        currentGarbageCount = PersistantData.Instance.playerData.VehicleGarbageCount;
    }

    void SaveParams()
    {
        //save parameters to savefile
        //maxGarbageCount
        //currentGarbageCount
        PersistantData.Instance.playerData.VehicleGarbageCount = currentGarbageCount;
        SaveSystem.SavePlayer(PersistantData.Instance.playerData);
    }
}
