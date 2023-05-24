using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public static class SaveSystem
{
    public static string filepathScene = "/" + SceneManager.GetActiveScene().name + ".game";
    public static string filepathPlayer = "/playerData.game";
    public static string shopFilepath = "/shop.game";
    public static void SaveSceneData(SceneData data)
    {
        filepathScene = "/" + SceneManager.GetActiveScene().name + ".game";
        string path = Application.persistentDataPath + filepathScene;

        SaveSceneData(data, path);
    }
    public static void SaveSceneData(SceneData data, string path)
    {
        using StreamWriter stream = new(path);
        string json = JsonUtility.ToJson(data);
        stream.Write(json);
    }
    public static SceneData LoadSceneData()
    {
        filepathScene = "/" + SceneManager.GetActiveScene().name + ".game";
        string path = Application.persistentDataPath + filepathScene;

        if (!File.Exists(path))
        {
            SaveSceneData(new SceneData());
        }
        SceneData playerData = new();

        using (StreamReader stream = new(path))
        {
            string json = stream.ReadToEnd();
            playerData = JsonUtility.FromJson<SceneData>(json);
        }

        return playerData;
    }
    public static void SavePlayerData(PlayerData data)
    {
        string path = Application.persistentDataPath + filepathPlayer;

        SavePlayerData(data, path);
    }

    public static void SavePlayerData(PlayerData data, string path)
    {
        using StreamWriter stream = new(path);
        string json = JsonUtility.ToJson(data);
        stream.Write(json);
    }


    public static void RemoveAllData()
    {
        filepathScene = "/" + SceneManager.GetActiveScene().name + ".game";
        string path = Application.persistentDataPath + filepathScene;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        path = Application.persistentDataPath + filepathPlayer;
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void RemoveSceneData()
    {
        filepathScene = "/" + SceneManager.GetActiveScene().name + ".game";
        string path = Application.persistentDataPath + filepathScene;
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


    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + filepathPlayer;

        if (!File.Exists(path))
        {
            SavePlayerData(new PlayerData());
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
