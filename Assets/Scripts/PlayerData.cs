using System;

[System.Serializable]
public class PlayerData
{
    public int SaveDataVersion = 2;

    public long Coins = 0;
    public int VolunteerCount = 1;

    public int CurrentGarbageCount = 0;
    public int Level = 1;
    public float Experience = 0;
    public float ExpLimit = 100;


    public PlayerData(long coins, int saveDataVer, int level, float exp, float limit)
    {
        this.Coins = coins;
        SaveDataVersion = saveDataVer;
        Level = level;
        Experience = exp;
        ExpLimit = limit;        
    }

    public PlayerData()
    {
        SaveDataVersion = 2;
        Coins = 0;
        VolunteerCount = 1;
        Level = 1;
        Experience = 0;
        ExpLimit = 100;
    }
}

