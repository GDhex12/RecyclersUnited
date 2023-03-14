using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;




    private void Start()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();

        PersistantData.Instance.GetLoadedData(loadedData);

        CurrencyManager.instance.SetCurrency(loadedData.Coins);

    }


}
