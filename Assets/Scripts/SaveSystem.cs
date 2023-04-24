using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string filepath = "/player.game";
    public static string shopFilepath = "/shop.game";
    public static void SavePlayer(PlayerData data)
    {
        string path = Application.persistentDataPath + filepath;

        using StreamWriter stream = new(path);
        string json = JsonUtility.ToJson(data);
        stream.Write(json);
    }

    public static void RemoveAllData()
    {
        string path = Application.persistentDataPath + filepath;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
    public static void RemoveAllVehicleData()
    {
        string path = Application.persistentDataPath + shopFilepath;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + filepath;

        if (!File.Exists(path))
        {
            SavePlayer(new PlayerData());
        }
        PlayerData playerData = new();

        using (StreamReader stream = new(path))
        {
            string json = stream.ReadToEnd();
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        return playerData;
    }

    public static void SaveShopData(VehicleDataList data)
    {
        string path = Application.persistentDataPath + shopFilepath;

        using StreamWriter stream = new(path);
        string json = JsonUtility.ToJson(data);
        stream.Write(json);
    }

    public static VehicleDataList LoadShopData()
    {
        string path = Application.persistentDataPath + shopFilepath;

        if (!File.Exists(path))
        {
            SaveShopData(new VehicleDataList());
        }
        VehicleDataList playerData = new();

        using (StreamReader stream = new(path))
        {
            string json = stream.ReadToEnd();
            playerData = JsonUtility.FromJson<VehicleDataList>(json);
        }
        return playerData;
    }

    public static bool IsVehicleDataCreated()
	{
        string path = Application.persistentDataPath + shopFilepath;
        if (!File.Exists(path))
        {
            return false;
        }
		else
		{
            return true;
		}
    }


}
