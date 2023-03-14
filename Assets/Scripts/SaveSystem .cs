using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string filepath = "/player.game";
    public static void SavePlayer(PlayerData data)
    {
        string path = Application.persistentDataPath + filepath;

        using (StreamWriter stream = new StreamWriter(path))
        {
            string json = JsonUtility.ToJson(data);
            stream.Write(json);
        }
        
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + filepath;

        if (!File.Exists(path))
        {
            SavePlayer(new PlayerData());
        }
        PlayerData playerData = new PlayerData();

        using (StreamReader stream = new StreamReader(path))
        {
            string json = stream.ReadToEnd();
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        return playerData;
    }
}
