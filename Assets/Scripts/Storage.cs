using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI storageUI;
    [SerializeField] private Image garbageFullnesImage;

    [SerializeField] int maxGarbageCount = 10;
    [SerializeField] int currentGarbageCount = 0;

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
        UpdateGarbage();
    }
    
    public void SetGarbageCount(int count)
    {
        currentGarbageCount = count;
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

    public int RemoveGarbage(int amount)
    {
        int garbageTaken;
        if (currentGarbageCount - amount <= 0)
        {
            garbageTaken = currentGarbageCount;
            currentGarbageCount = 0;
        }
        else
        {
            garbageTaken = amount;
            currentGarbageCount -= amount;
        }     
        UpdateGarbage();
        return garbageTaken;
    }

    public void RemoveAllGarbage()//int
    {
        currentGarbageCount = 0;
        UpdateGarbage();
        //return amount;
    }

    public bool IsFull()
    {
        return !(currentGarbageCount < maxGarbageCount);
    }

    public bool IsEmpty()
    {
        return !(currentGarbageCount > 0);
    }

    void UpdateGarbage()
    {
        // Update storage UI
        if (storageUI != null)
        {
            storageUI.text = StorageStringFormat();
        }
        // Update save data
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
        currentGarbageCount = PersistantData.Instance.sceneData.StorageGarbageCount;
        UpdateGarbage();
    }

    void SaveParams()
    {
        //save parameters to savefile
        //maxGarbageCount
        //currentGarbageCount
        PersistantData.Instance.sceneData.StorageGarbageCount = currentGarbageCount;
        SaveSystem.SaveSceneData(PersistantData.Instance.sceneData);
    }
}
