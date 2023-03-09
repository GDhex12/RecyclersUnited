using System;

[System.Serializable]
public class PlayerData
{
    public long Coins = 0;
    public int VolunteerCount = 1;

    public PlayerData(long coins)
    {
        this.Coins = coins;
    }

    public PlayerData()
    {
        Coins = 0;
        VolunteerCount = 1;
    }
}

