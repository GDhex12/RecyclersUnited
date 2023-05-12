using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int SaveDataVersion = 5;

    public bool NewGameTutorial = true;

    [Header("Currency")]
    public long Coins = 0;

    [Header("Player xp and levels")]
    public int Level = 1;
    public float Experience = 0;
    public float ExpLimit = 50;

    [Header("Unlocked Areas")]
    public bool[] isAreaUnlocked = new bool[10];


    public PlayerData(long coins, int saveDataVer, int level, float exp, float limit, bool newgame)
    {
        Coins = coins;
        SaveDataVersion = saveDataVer;
        Level = level;
        Experience = exp;
        ExpLimit = limit;
        NewGameTutorial = newgame;
    }

    public PlayerData()
    {
        SaveDataVersion = 5;

        NewGameTutorial = true;

        //Currency
        Coins = 0;

        //Player xp and levels
        Level = 1;
        Experience = 0;
        ExpLimit = 50;

        //Areas
        isAreaUnlocked = new bool[10];
        isAreaUnlocked[0] = true;
    }
}

