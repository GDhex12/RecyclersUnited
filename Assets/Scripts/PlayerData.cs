using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int SaveDataVersion = 4;

    [Header("Currency")]
    public long Coins = 0;
    public int StorageGarbageCount = 0;
    public int VehicleGarbageCount = 0;

    [Header("Bought object counts")]
    public int VolunteerPickerCount = 1;
    public int VolunteerLoaderCount = 1;

    [Header("Player xp and levels")]
    public int Level = 1;
    public float Experience = 0;
    public float ExpLimit = 50;

    [Header("Upgrade xp's and levels")]
    public int VehicleCapacityCurrentLevel = 1;
    public int StorageCapacityCurrentLevel = 1;
    public int PickerSpeedCurrentLevel = 1;
    public int LoaderSpeedCurrentLevel = 1;
    public int LoaderBagCurrentLevel = 1;

    public PlayerData(long coins, int saveDataVer, int level, float exp, float limit)
    {
        Coins = coins;
        SaveDataVersion = saveDataVer;
        Level = level;
        Experience = exp;
        ExpLimit = limit;
    }

    public PlayerData()
    {
        SaveDataVersion = 4;

        //Currency
        Coins = 0;
        StorageGarbageCount = 0;
        VehicleGarbageCount = 0;

        //Bought object counts
        VolunteerPickerCount = 1;
        VolunteerLoaderCount = 1;

        //Player xp and levels
        Level = 1;
        Experience = 0;
        ExpLimit = 50;

        //Upgrade xp's and levels
        VehicleCapacityCurrentLevel = 1;
        StorageCapacityCurrentLevel = 1;
        PickerSpeedCurrentLevel = 1;
        LoaderSpeedCurrentLevel = 1;
        LoaderBagCurrentLevel = 1;
    }
}


