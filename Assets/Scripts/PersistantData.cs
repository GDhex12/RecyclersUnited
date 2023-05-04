using UnityEngine;

public class PersistantData : MonoBehaviour
{
    public static PersistantData Instance { get; private set; }
    public PlayerData playerData = new();
    public SceneData sceneData = new();
    public int SaveDataVerToCheck = 5;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void GetLoadedData(SceneData data)
    {
        sceneData = data;
    }
    public void GetLoadedData(PlayerData data)
    {
        playerData = data;
    }
}
