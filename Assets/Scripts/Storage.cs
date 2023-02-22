using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Storage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI storageUI;

    int mapID;
    [SerializeField] int maxGarbageCount = 10;
    [SerializeField] int currentGarbageCount = 0;

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

    public bool IsFull()
    {
        return !(currentGarbageCount < maxGarbageCount);
    }

    void UpdateGarbage()
    {
        // Update storage UI
        if (storageUI != null)
        {
            storageUI.text = StorageStringFormat();
        }
        // Update save data
        SaveParams();
    }

    string StorageStringFormat()
    {
        return $"{currentGarbageCount}/{maxGarbageCount}";
    }

    void GetParamsFromSave()
    {
        //get parameters from savefile
            //maxGarbageCount
            //currentGarbageCount
            //mapID
    }

    void SaveParams()
    {
        //save parameters to savefile
            //maxGarbageCount
            //currentGarbageCount
    }
}
