using System;

[System.Serializable]
public class PlayerData
{
    public long Coins = 0;
    public int VolunteerCount = 1;
<<<<<<< Updated upstream
=======
    public int CurrentGarbageCount = 0;
    public int Level = 1;
    public float Experience = 0;
    public float ExpLimit = 100;
>>>>>>> Stashed changes

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

