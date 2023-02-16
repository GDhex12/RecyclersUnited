using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CurrencyManager currencyManager;
    private void Awake()
    {
        PlayerData loadedData = SaveSystem.LoadPlayer();
        currencyManager.SetCurrency(loadedData.Coins);
    }
}
