using System;

[System.Serializable]
public class PlayerData
{
    public long Coins = 0;

    public PlayerData(long coins)
    {
        this.Coins = coins;
    }
    public PlayerData()
    {
        Coins = 0;
    }
}

