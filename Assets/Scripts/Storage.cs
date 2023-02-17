using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
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

    public bool IsFull()
    {
        return !(currentGarbageCount < maxGarbageCount);
    }

    void UpdateGarbage()
    {
        // Update scene data
        // Update storage UI
    }
}
