using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VehicleSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI vehicleUI;
    [SerializeField] private Image garbageFullnesImage;

    [Header("Vehicle capacity")]
    [SerializeField] private int maxGarbageCount = 25;
    public int currentGarbageCount = 0;

    [Header("Money")]
    [SerializeField] private int moneyPerGarbage = 10;

    
    private void Start()
    {
        currentGarbageCount = PersistantData.Instance.sceneData.VehicleGarbageCount;

       
    }

    public int GetGarbageCount()
    {
        return currentGarbageCount;
    }

    public void SetGarbageCount(int count)
    {
        currentGarbageCount = count;
        UpdateGarbage();
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
        {
            UpdateGarbageUI();
            return;
        }    
        else
        {
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
        {
            Debug.Log("2");
            return;
        }
        if (!storage.IsEmpty() && amount <= storage.GetGarbageCount())
        {
            storage.RemoveGarbage(amount);
            AddGarbage(amount);
        }
        UpdateGarbageUI();
    }

    public void ExchangeGarbageToMoney(int garbageCount)
    {
        CurrencyManager.instance.AddCurrency(garbageCount * moneyPerGarbage);
    }

    public string GetGarbageToMoneyToString(int garbageCount)
    {
        return CurrencyManager.instance.CurrencyStringFormat(garbageCount * moneyPerGarbage);
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
        UpdateGarbageUI();
        SaveParams();
    }

    void UpdateGarbageUI()
    {
        garbageFullnesImage.fillAmount = (float)currentGarbageCount / (float)maxGarbageCount;
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
        currentGarbageCount = PersistantData.Instance.sceneData.VehicleGarbageCount;
        UpdateGarbage();
    }

    void SaveParams()
    {
        //save parameters to savefile
        //maxGarbageCount
        //currentGarbageCount
        PersistantData.Instance.sceneData.VehicleGarbageCount = currentGarbageCount;
        SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
    }
}
