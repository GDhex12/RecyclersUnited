using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI vehicleUI;

    [Header("Vehicle capacity")]
    [SerializeField] int maxGarbageCount = 5;
    [SerializeField] int currentGarbageCount = 0;

    [Header("Money")]
    [SerializeField] long moneyPerGarbage = 10;

    private void Start()
    {
        UpdateGarbage();
    }

    public int GetGarbageCount()
    {
        return currentGarbageCount;
    }

    public void SetMaxGarbageCount(int count)
    {
        maxGarbageCount = count;
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

    public void RemoveAllGarbage()//int
    {
        int amount = currentGarbageCount;
        currentGarbageCount = 0;
        UpdateGarbage();
        //return amount;
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
        
    }

    string StorageStringFormat()
    {
        return $"{currentGarbageCount}/{maxGarbageCount}";
    }
}
