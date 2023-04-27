using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int SaveDataVersion = 5;

    [Header("Currency")]
    public long Coins = 0;

    [Header("Player xp and levels")]
    public int Level = 1;
    public float Experience = 0;
    public float ExpLimit = 100;

    [Header("Unlocked Areas")]
    public bool[] isAreaUnlocked = new bool[10];


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
        SaveDataVersion = 5;

        //Currency
        Coins = 0;

        //Player xp and levels
        Level = 1;
        Experience = 0;
        ExpLimit = 100;

        //Areas
        isAreaUnlocked = new bool[10];
        isAreaUnlocked[0] = true;
    }
}

