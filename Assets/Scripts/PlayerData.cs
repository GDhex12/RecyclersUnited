using System;

[System.Serializable]
public class PlayerData
{
    public int SaveDataVersion = 2;

    public long Coins = 0;
    public int VolunteerCount = 1;

    public PlayerData(long coins, int saveDataVer)
    {
        this.Coins = coins;
        SaveDataVersion = saveDataVer;
    }

    public PlayerData()
    {
        SaveDataVersion = 2;
        Coins = 0;
        VolunteerCount = 1;
    }
}

