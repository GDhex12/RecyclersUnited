using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickAddCoins : MonoBehaviour
{
    [Header("Add Coins")]
    [SerializeField] private int coinsFrom = 10;
    [SerializeField] private int coinsTo = 100;
    public void OnClick()
    {
        if (gameObject.activeSelf)
        {
            int rngSelection = Random.Range(coinsFrom, coinsTo);
            CurrencyManager.instance.AddCurrency(rngSelection);
            gameObject.GetComponent<CoinFlightHelper>().RewardCoins(rngSelection / 10);
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }

}
