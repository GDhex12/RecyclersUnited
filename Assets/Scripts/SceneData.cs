using System;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int SaveDataVersion = 4;

    [Header("Current Collected Amount")]
    public int StorageGarbageCount = 0;
    public int VehicleGarbageCount = 0;

    [Header("Bought object counts")]
    public int VolunteerPickerCount = 1;
    public int VolunteerLoaderCount = 1;

    [Header("Upgrade xp's and levels")]
    public int VehicleCapacityCurrentLevel = 1;
    public int StorageCapacityCurrentLevel = 1;
    public int PickerSpeedCurrentLevel = 1;
    public int LoaderSpeedCurrentLevel = 1;
    public int LoaderBagCurrentLevel = 1;

    public SceneData(int saveDataVer)
    {
        SaveDataVersion = saveDataVer;
    }

    public SceneData()
    {
        SaveDataVersion = 4;

        //Current Collected Amount
        StorageGarbageCount = 0;
        VehicleGarbageCount = 0;

        //Bought object counts
        VolunteerPickerCount = 1;
        VolunteerLoaderCount = 1;

        //Upgrade xp's and levels
        VehicleCapacityCurrentLevel = 1;
        StorageCapacityCurrentLevel = 1;
        PickerSpeedCurrentLevel = 1;
        LoaderSpeedCurrentLevel = 1;
        LoaderBagCurrentLevel = 1;
    }
}


